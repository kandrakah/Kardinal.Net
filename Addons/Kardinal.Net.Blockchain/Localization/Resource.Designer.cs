//------------------------------------------------------------------------------
// <auto-generated>
//     O código foi gerado por uma ferramenta.
//     Versão de Tempo de Execução:4.0.30319.42000
//
//     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
//     o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kardinal.Net.Blockchain.Localization {
    using System;
    
    
    /// <summary>
    ///   Uma classe de recurso de tipo de alta segurança, para pesquisar cadeias de caracteres localizadas etc.
    /// </summary>
    // Essa classe foi gerada automaticamente pela classe StronglyTypedResourceBuilder
    // através de uma ferramenta como ResGen ou Visual Studio.
    // Para adicionar ou remover um associado, edite o arquivo .ResX e execute ResGen novamente
    // com a opção /str, ou recrie o projeto do VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        /// <summary>
        ///   Retorna a instância de ResourceManager armazenada em cache usada por essa classe.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Kardinal.Net.Blockchain.Localization.Resource", typeof(Resource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Substitui a propriedade CurrentUICulture do thread atual para todas as
        ///   pesquisas de recursos que usam essa classe de recurso de tipo de alta segurança.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a Null or invalid deserialization data..
        /// </summary>
        internal static string ERROR_BLOCKCHAIN_INVALID_DESSERIALIZATION {
            get {
                return ResourceManager.GetString("ERROR_BLOCKCHAIN_INVALID_DESSERIALIZATION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a The link [{index}][{hash}] does not belong to this blockchain..
        /// </summary>
        internal static string ERROR_BLOCKCHAIN_INVALID_LINK {
            get {
                return ResourceManager.GetString("ERROR_BLOCKCHAIN_INVALID_LINK", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a Unable to serialize blockchain that has no links..
        /// </summary>
        internal static string ERROR_BLOCKCHAIN_NO_LINKS {
            get {
                return ResourceManager.GetString("ERROR_BLOCKCHAIN_NO_LINKS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a The object&apos;s current hash does not match the one recorded in the blockchain..
        /// </summary>
        internal static string ERROR_HASH_MISMATCH {
            get {
                return ResourceManager.GetString("ERROR_HASH_MISMATCH", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a The hash entered does not match any item in the chain..
        /// </summary>
        internal static string ERROR_HASH_NOT_FOUND {
            get {
                return ResourceManager.GetString("ERROR_HASH_NOT_FOUND", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a Previous blockchain Hash does not match record in current object: [INDEX: {index}].
        /// </summary>
        internal static string ERROR_HASH_PREVIOUS_MISMATCH {
            get {
                return ResourceManager.GetString("ERROR_HASH_PREVIOUS_MISMATCH", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a The index informed does not match any item in the chain..
        /// </summary>
        internal static string ERROR_INDEX_NOT_FOUND {
            get {
                return ResourceManager.GetString("ERROR_INDEX_NOT_FOUND", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a Serializer identification key not found..
        /// </summary>
        internal static string ERROR_SERIALIZER_KEY_NOT_FOUND {
            get {
                return ResourceManager.GetString("ERROR_SERIALIZER_KEY_NOT_FOUND", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a The serializer identification key is null or invalid..
        /// </summary>
        internal static string ERROR_SERIALIZER_KEY_NULL {
            get {
                return ResourceManager.GetString("ERROR_SERIALIZER_KEY_NULL", resourceCulture);
            }
        }
    }
}
