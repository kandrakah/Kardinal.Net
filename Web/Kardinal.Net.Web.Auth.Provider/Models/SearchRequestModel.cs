namespace Kardinal.Net.Web.Auth
{
    /// <summary>
    /// Classe de modelo de busca.
    /// </summary>
    public class SearchRequestModel
    {
        /// <summary>
        /// Itens à serem ignorados.
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// Itens à serem obtidos.
        /// </summary>
        public int Take { get; set; }

        /// <summary>
        /// Termos de busca.
        /// </summary>
        public string Terms { get; set; }        

        /// <summary>
        /// Método que traz uma cadeia de caracteres que representa o objeto atual.
        /// </summary>
        /// <returns>Cadeia de caracteres que representa o objeto atual.</returns>
        public override string ToString()
        {
            return $"[{this.Skip}/{this.Take}]{this.Terms}".Trim();
        }
    }
}
