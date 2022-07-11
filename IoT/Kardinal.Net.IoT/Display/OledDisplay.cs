using Iot.Device.Ssd13xx;
using System.Diagnostics.CodeAnalysis;

namespace Kardinal.Net.IoT.Display
{
    /// <summary>
    /// Classe de operação do display Oled.
    /// </summary>
    public sealed class OledDisplay : IDisposable
    {
        /// <summary>
        /// Instância do dispositivo.
        /// </summary>
        private readonly Ssd1306 _device;

        /// <summary>
        /// Largura da tela.
        /// </summary>
        public readonly uint _width;

        /// <summary>
        /// Altura da tela.
        /// </summary>
        public readonly uint _height;

        /// <summary>
        /// Número de páginas da tela.
        /// </summary>
        public readonly uint _pages;

        /// <summary>
        /// Buffer de dados.
        /// </summary>
        private readonly byte[,] _buffer;

        /// <summary>
        /// Buffer de dados da tela.
        /// </summary>
        private readonly byte[] _displayBuffer;

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="driver">Instância do dispositivo.</param>
        /// <param name="width">Largura da tela.</param>
        /// <param name="height">Altura da tela.</param>
        public OledDisplay([NotNull] Ssd1306 driver, uint width = 128, uint height = 64)
        {
            this._device = driver;
            this._width = width;
            this._height = height;

            this._pages = _height / 8;
            this._buffer = new byte[this._width, this._pages];
            this._displayBuffer = new byte[this._width * this._pages];
        }

        /// <summary>
        /// Método que inicializa a tela.
        /// </summary>
        public void Initialize()
        {
            try
            {
                this.Clear(true);
                this.SendCommand(new MemoryAddressModeCommand());
                this.SendCommand(new SegremapCommand());
                this.SendCommand(new ComScanDirCommand());
                this.SendCommand(new PowerCommand(true));
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Método que atualiza os dados da tela.
        /// </summary>
        public void Update()
        {
            var index = 0;
            for (var y = 0; y < this._pages; y++)
            {
                for (var x = 0; x < this._width; x++)
                {
                    this._displayBuffer[index] = this._buffer[x, y];
                    index++;
                }
            }

            this.SendCommand(new ResetColumnAddressCommand());
            this.SendCommand(new ResetPageAddressCommand());
            this.SendData(new DisplayData(this._displayBuffer));
        }

        /// <summary>
        /// Método que adiciona um texto à tela.
        /// </summary>
        /// <param name="text">Texto à ser adicionado a tela.</param>
        /// <param name="column">Coluna onde o texto deve ser adicionado.</param>
        /// <param name="row">Linha onde o texto deve ser adicionado.</param>
        public void DrawText([NotNull] string text, uint column, uint row)
        {
            uint charWidth = 0;
            foreach (var character in text)
            {
                charWidth = this.DrawCharacter(character, column, row);
                column += charWidth;
                if (charWidth == 0)
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Método que adicona uma imagem à tela.
        /// </summary>
        /// <param name="image">Dados da imagem à ser desenhada.</param>
        /// <param name="column">Coluna onde a imagem deve ser adicionada.</param>
        /// <param name="row">Linha onde a imagem deve ser adicionada.</param>
        /// <returns></returns>
        public uint DrawImage(DisplayImage image, uint column, uint row)
        {
            var maxRowValue = (this._pages / image.ImageHeightBytes) - 1;
            var maxColumnValue = this._width;
            if (row > maxRowValue)
            {
                return 0;
            }

            if ((column + image.ImageWidthPx + DisplayFontTable.FontCharSpacing) > maxColumnValue)
            {
                return 0;
            }

            var dataIndex = 0;
            var startPage = row * 2;
            var endPage = startPage + image.ImageHeightBytes;
            var startColumn = column;
            var endColumn = startColumn + image.ImageWidthPx;
            var currentPage = (uint)0;
            var currentColumn = (uint)0;

            for (currentColumn = startColumn; currentColumn < endColumn; currentColumn++)
            {
                for (currentPage = startPage; currentPage < endPage; currentPage++)
                {
                    this._buffer[currentColumn, currentPage] = image.ImageData[dataIndex];
                    dataIndex++;
                }
            }

            return currentColumn - startColumn;
        }

        /// <summary>
        /// Método que escreve um pixel na tela.
        /// </summary>
        /// <param name="x">Eixo X onde o pixel deve ser acionado.</param>
        /// <param name="y">Eixo Y onde o pixel deve ser acionado.</param>
        /// <param name="colored">Indica que o pixel à ser acionado deve ser colorido.</param>
        public void DrawPixel(int x, int y, bool colored = false)
        {
            if (x >= this._width || y >= this._height)
            {
                return;
            }
            var page = y / 8;
            var byteToDraw = (byte)(1 << (y % 8));
            if (colored)
            {
                this._buffer[page, x] |= byteToDraw;
            }
            else
            {
                this._buffer[page, x] &= byteToDraw;
            }
        }

        /// <summary>
        /// Método que escreve uma linha na tela.
        /// </summary>
        /// <param name="xi">Posição X inicial da tela.</param>
        /// <param name="yi">Posição Y inicial da tela.</param>
        /// <param name="xf">Posição X final da tela.</param>
        /// <param name="yf">Posição Y final da tela.</param>
        /// <param name="colored">Indica que a linha à ser desenhada deve ser colorida.</param>
        public void DrawLine(int xi, int yi, int xf, int yf, bool colored = false)
        {
            var steep = Math.Abs(yf - yi) > Math.Abs(xf - xi);
            if (steep)
            {
                int t;
                t = xi; // swap x0 and y0
                xi = yi;
                yi = t;
                t = xf; // swap x1 and y1
                xf = yf;
                yf = t;
            }
            if (xi > xf)
            {
                int t;
                t = xi; // swap x0 and x1
                xi = xf;
                xf = t;
                t = yi; // swap y0 and y1
                yi = yf;
                yf = t;
            }

            int dx = xf - xi;
            int dy = System.Math.Abs(yf - yi);
            int error = dx / 2;
            int ystep = (yi < yf) ? 1 : -1;
            int y = yi;

            for (int x = xi; x <= xf; x++)
            {
                this.DrawPixel((steep ? y : x), (steep ? x : y), colored);
                error = error - dy;
                if (error < 0)
                {
                    y += ystep;
                    error += dx;
                }
            }
        }

        /// <summary>
        /// Método que escreve uma linha vertical na tela.
        /// </summary>
        /// <param name="x">Posição X inicial da linha.</param>
        /// <param name="y">Posição Y inicial da linha.</param>
        /// <param name="height">Altura da linha.</param>
        /// <param name="colored">Indica que a linha à ser desenhada deve ser colorida.</param>
        public void DrawVerticalLine(int x, int y, int height, bool colored = false)
        {
            this.DrawLine(x, y, x, y + height - 1, colored);
        }

        /// <summary>
        /// Método que escreve uma linha horizontal na tela.
        /// </summary>
        /// <param name="x">Posição X inicial da linha.</param>
        /// <param name="y">Posição Y inicial da linha.</param>
        /// <param name="width">Comprimento da linha.</param>
        /// <param name="colored">Indica que a linha à ser desenhada deve ser colorida.</param>
        public void DrawHorizontalLine(int x, int y, int width, bool colored = false)
        {
            this.DrawLine(x, y, x + width - 1, y, colored);
        }

        /// <summary>
        /// Método que escreve um retângulo na tela.
        /// </summary>
        /// <param name="xLeft">Posição X inicial.</param>
        /// <param name="yTop">Posição Y inicial.</param>
        /// <param name="width">Largura do retângulo.</param>
        /// <param name="height">Altura do retângulo.</param>
        /// <param name="colored">Indica que o retângulo à ser desenhado deve ser colorida.</param>
        public void DrawRectangle(int xLeft, int yTop, int width, int height, bool colored = false)
        {
            width--;
            height--;
            this.DrawLine(xLeft, yTop, xLeft + width, yTop, colored);
            this.DrawLine(xLeft + width, yTop, xLeft + width, yTop + height, colored);
            this.DrawLine(xLeft + width, yTop + height, xLeft, yTop + height, colored);
            this.DrawLine(xLeft, yTop, xLeft, yTop + height, colored);
        }

        /// <summary>
        /// Método que escreve um retângulo preenchido na tela.
        /// </summary>
        /// <param name="xLeft">Posição X inicial.</param>
        /// <param name="yTop">Posição Y inicial.</param>
        /// <param name="width">Largura do retângulo.</param>
        /// <param name="height">Altura do retângulo.</param>
        /// <param name="colored">Indica que o retângulo à ser desenhado deve ser colorida.</param>
        public void DrawFilledRectangle(int xLeft, int yTop, int width, int height, bool colored = false)
        {
            width--;
            height--;
            for (int i = 0; i <= height; i++)
            {
                this.DrawLine(xLeft, yTop + i, xLeft + width, yTop + i, colored);
            }
        }

        /// <summary>
        /// Método que escreve um retângulo com cantos arredondados na tela.
        /// </summary>
        /// <param name="x">Posição X inicial.</param>
        /// <param name="y">Posição Y inicial.</param>
        /// <param name="width">Largura do retângulo.</param>
        /// <param name="height">Altura do retângulo.</param>
        /// <param name="radius">Raio do círculo.</param>
        /// <param name="colored">Indica que o retângulo à ser desenhado deve ser colorida.</param>
        public void DrawRoundRectangle(int x, int y, int width, int height, int radius, bool colored = false)
        {
            this.DrawHorizontalLine(x + radius, y, width - 2 * radius, colored);
            this.DrawHorizontalLine(x + radius, y + height - 1, width - 2 * radius, colored);
            this.DrawVerticalLine(x, y + radius, height - 2 * radius, colored);
            this.DrawVerticalLine(x + width - 1, y + radius, height - 2 * radius, colored);


            this.DrawCircleHelper(x + radius, y + radius, radius, 1, colored);
            this.DrawCircleHelper(x + width - radius - 1, y + radius, radius, 2, colored);
            this.DrawCircleHelper(x + width - radius - 1, y + height - radius - 1, radius, 4, colored);
            this.DrawCircleHelper(x + radius, y + height - radius - 1, radius, 8, colored);
        }

        /// <summary>
        /// Método que escreve um retângulo com cantos arredondados preenchido na tela.
        /// </summary>
        /// <param name="x">Posição X inicial.</param>
        /// <param name="y">Posição Y inicial.</param>
        /// <param name="width">Largura do retângulo.</param>
        /// <param name="height">Altura do retângulo.</param>
        /// <param name="radius">Raio do círculo.</param>
        /// <param name="colored">Indica que o retângulo à ser desenhado deve ser colorida.</param>
        public void DrawRoundFilledRectangle(int x, int y, int width, int height, int radius, bool colored = false)
        {

            // smarter version
            for (int i = x + radius; i < x + radius + (width - 2 * radius); i++)
            {
                this.DrawVerticalLine(i, y, height, colored);
            }

            // draw four corners
            this.FillCircleHelper(x + width - radius - 1, y + radius, radius, 1, height - 2 * radius - 1, colored);
            this.FillCircleHelper(x + radius, y + radius, radius, 2, height - 2 * radius - 1, colored);

        }

        /// <summary>
        /// Método que desenha um circulo na tela.
        /// </summary>
        /// <param name="centerX">Posição X do centro do círculo.</param>
        /// <param name="centerY">Posição Y do centro do círculo.</param>
        /// <param name="radius">Raio do círculo.</param>
        /// <param name="colored">Indica que o circulo à ser desenhado deve ser colorida.</param>
        public void DrawCircle(int centerX, int centerY, int radius, bool colored = false)
        {
            radius--;
            int d = (5 - radius * 4) / 4;
            int x = 0;
            int y = radius;
            do
            {

                this.DrawPixel(centerX + x, centerY + y, colored);
                this.DrawPixel(centerX + x, centerY - y, colored);
                this.DrawPixel(centerX - x, centerY + y, colored);
                this.DrawPixel(centerX - x, centerY - y, colored);
                this.DrawPixel(centerX + y, centerY + x, colored);
                this.DrawPixel(centerX + y, centerY - x, colored);
                this.DrawPixel(centerX - y, centerY + x, colored);
                this.DrawPixel(centerX - y, centerY - x, colored);
                if (d < 0)
                {
                    d += 2 * x + 1;
                }
                else
                {
                    d += 2 * (x - y) + 1;
                    y--;
                }
                x++;

            } while (x <= y);
        }

        /// <summary>
        /// Método que desenha um circulo preenchido na tela.
        /// </summary>
        /// <param name="centerX">Posição X do centro do círculo.</param>
        /// <param name="centerY">Posição Y do centro do círculo.</param>
        /// <param name="radius">Raio do círculo.</param>
        /// <param name="colored">Indica que o circulo preencido à ser desenhado deve ser colorida.</param>
        public void DrawFilledCircle(int centerX, int centerY, int radius, bool colored = false)
        {
            radius--;
            int d = (5 - radius * 4) / 4;
            int x = 0;
            int y = radius;
            do
            {
                this.DrawLine(centerX + x, centerY + y, centerX - x, centerY + y, colored);
                this.DrawLine(centerX + x, centerY - y, centerX - x, centerY - y, colored);

                this.DrawLine(centerX - y, centerY + x, centerX + y, centerY + x, colored);
                this.DrawLine(centerX - y, centerY - x, centerX + y, centerY - x, colored);
                if (d < 0)
                {
                    d += 2 * x + 1;
                }
                else
                {
                    d += 2 * (x - y) + 1;
                    y--;
                }
                x++;
            } while (x <= y);
        }

        /// <summary>
        /// Método que desenha um triângulo na tela.
        /// </summary>
        /// <param name="xa">Posição X do primeiro vértice.</param>
        /// <param name="ya">Posição Y do primeiro vértice.</param>
        /// <param name="xb">Posição X do segundo vértice.</param>
        /// <param name="yb">Posição Y do segundo vértice.</param>
        /// <param name="xc">Posição X do terceiro vértice.</param>
        /// <param name="yc">Posição Y do terceiro vértice.</param>
        /// <param name="colored">Indica que o circulo preencido à ser desenhado deve ser colorida.</param>
        public void DrawTriangle(int xa, int ya, int xb, int yb, int xc, int yc, bool colored = false)
        {
            this.DrawLine(xa, ya, xb, yb, colored);
            this.DrawLine(xb, yb, xc, yc, colored);
            this.DrawLine(xc, yc, xa, ya, colored);
        }

        /// <summary>
        /// Método que limpa o conteúdo da tela.
        /// </summary>
        /// <param name="turnOff">Indica que a tela deve ser desligada após a limpeza.</param>
        public void Clear(bool turnOff = false)
        {
            Array.Clear(this._buffer);
            this.SendCommand(new ResetColumnAddressCommand());
            this.SendCommand(new ResetPageAddressCommand());
            if (turnOff)
            {
                this.SendCommand(new PowerCommand(false));
            }
        }

        /// <summary>
        /// Método que envia um comando à tela.
        /// </summary>
        /// <param name="command">Comando à ser enviado à tela.</param>
        private void SendCommand([NotNull] DisplayCommand command)
        {
            this._device.SendCommand(command);
        }

        /// <summary>
        /// Método que envia dados à tela.
        /// </summary>
        /// <param name="data">Dados à serem adicionados à tela.</param>
        private void SendData([NotNull] DisplayData data)
        {
            this._device.SendData(data.GetBytes());
        }

        /// <summary>
        /// Método que escreve um caractere na tela.
        /// </summary>
        /// <param name="character">Caractere à ser escrito.</param>
        /// <param name="column">Coluna onde o caractere deve ser adicionado.</param>
        /// <param name="row">Linha onde o caractere deve ser adicionado.</param>
        /// <returns></returns>
        private uint DrawCharacter(char character, uint column, uint row)
        {
            var characterDescriptor = DisplayFontTable.GetCharacterDescriptor(character);
            if (characterDescriptor == null)
            {
                return 0;
            }

            var MaxRowValue = (this._pages / DisplayFontTable.FontHeightBytes) - 1;
            var MaxColValue = this._width;
            if (row > MaxRowValue)
            {
                return 0;
            }
            if ((column + characterDescriptor.WidthPixels + DisplayFontTable.FontCharSpacing) > MaxColValue)
            {
                return 0;
            }

            uint dataIndex = 0;
            uint startPage = row * 2;
            uint endPage = startPage + characterDescriptor.HeightBytes;
            uint startColumn = column;
            uint endColumn = startColumn + characterDescriptor.WidthPixels;
            uint currentPage = 0;
            uint currentColumn = 0;

            for (currentPage = startPage; currentPage < endPage; currentPage++)
            {
                for (currentColumn = startColumn; currentColumn < endColumn; currentColumn++)
                {
                    this._buffer[currentColumn, currentPage] = characterDescriptor.Data[dataIndex];
                    dataIndex++;
                }
            }

            for (currentPage = startPage; currentPage < endPage; currentPage++)
            {
                for (; currentColumn < endColumn + DisplayFontTable.FontCharSpacing; currentColumn++)
                {
                    this._buffer[currentColumn, currentPage] = 0x00;
                }
            }

            return currentColumn - startColumn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xi">Posição X inicial.</param>
        /// <param name="yi">Posição Y inicial.</param>
        /// <param name="radius">Raio.</param>
        /// <param name="cornername"></param>
        /// <param name="delta"></param>
        /// <param name="colored">Indica que o circulo preencido à ser desenhado deve ser colorida.</param>
        private void DrawCircleHelper(int xi, int yi, int radius, int cornername, bool colored = false)
        {
            int f = 1 - radius;
            int ddF_x = 1;
            int ddF_y = -2 * radius;
            int x = 0;
            int y = radius;

            while (x < y)
            {
                if (f >= 0)
                {
                    y--;
                    ddF_y += 2;
                    f += ddF_y;
                }
                x++;
                ddF_x += 2;
                f += ddF_x;
                if ((cornername & 0x4) != 0)
                {
                    this.DrawPixel(xi + x, yi + y, colored);
                    this.DrawPixel(xi + y, yi + x, colored);
                }
                if ((cornername & 0x2) != 0)
                {
                    this.DrawPixel(xi + x, yi - y, colored);
                    this.DrawPixel(xi + y, yi - x, colored);
                }
                if ((cornername & 0x8) != 0)
                {
                    this.DrawPixel(xi - y, yi + x, colored);
                    this.DrawPixel(xi - x, yi + y, colored);
                }
                if ((cornername & 0x1) != 0)
                {
                    this.DrawPixel(xi - y, yi - x, colored);
                    this.DrawPixel(xi - x, yi - y, colored);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xi">Posição X inicial.</param>
        /// <param name="yi">Posição Y inicial.</param>
        /// <param name="radius">Raio.</param>
        /// <param name="cornername"></param>
        /// <param name="delta"></param>
        /// <param name="colored">Indica que o circulo preencido à ser desenhado deve ser colorida.</param>
        private void FillCircleHelper(int xi, int yi, int radius, int cornername, int delta, bool colored = false)
        {

            int f = 1 - radius;
            int ddF_x = 1;
            int ddF_y = -2 * radius;
            int x = 0;
            int y = radius;

            while (x < y)
            {
                if (f >= 0)
                {
                    y--;
                    ddF_y += 2;
                    f += ddF_y;
                }
                x++;
                ddF_x += 2;
                f += ddF_x;

                if ((cornername & 0x1) != 0)
                {
                    this.DrawVerticalLine(xi + x, yi - y, 2 * y + 1 + delta, colored);
                    this.DrawVerticalLine(xi + y, yi - x, 2 * x + 1 + delta, colored);
                }
                if ((cornername & 0x2) != 0)
                {
                    this.DrawVerticalLine(xi - x, yi - y, 2 * y + 1 + delta, colored);
                    this.DrawVerticalLine(xi - y, yi - x, 2 * x + 1 + delta, colored);
                }
            }
        }

        /// <summary>
        /// Método para limpeza do objeto ao encerrar.
        /// </summary>
        public void Dispose()
        {
            this.Clear(true);
        }
    }
}
