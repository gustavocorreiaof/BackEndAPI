﻿//------------------------------------------------------------------------------
// <auto-generated>
//     O código foi gerado por uma ferramenta.
//     Versão de Tempo de Execução:4.0.30319.42000
//
//     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
//     o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Core.Common.Util.Msgs {
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
    public class ApiMsg {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ApiMsg() {
        }
        
        /// <summary>
        ///   Retorna a instância de ResourceManager armazenada em cache usada por essa classe.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Core.Common.Util.Msgs.ApiMsg", typeof(ApiMsg).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a The TransferValue must be greater than 0..
        /// </summary>
        public static string EX001 {
            get {
                return ResourceManager.GetString("EX001", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a There is already a user with this TaxNumber..
        /// </summary>
        public static string EX002 {
            get {
                return ResourceManager.GetString("EX002", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a There is already a user with this Email..
        /// </summary>
        public static string EX003 {
            get {
                return ResourceManager.GetString("EX003", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a Transaction not authorized..
        /// </summary>
        public static string EX004 {
            get {
                return ResourceManager.GetString("EX004", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a There is no registered user with the FromTaxNumber provided..
        /// </summary>
        public static string EX005 {
            get {
                return ResourceManager.GetString("EX005", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a There is no registered user with the ToTaxNumber provided..
        /// </summary>
        public static string EX006 {
            get {
                return ResourceManager.GetString("EX006", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a Users of type CNPJ cannot make transfers..
        /// </summary>
        public static string EX007 {
            get {
                return ResourceManager.GetString("EX007", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a The source account does not have sufficient funds for the transaction..
        /// </summary>
        public static string EX008 {
            get {
                return ResourceManager.GetString("EX008", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a User successfully created..
        /// </summary>
        public static string INF001 {
            get {
                return ResourceManager.GetString("INF001", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a Transfer completed successfully..
        /// </summary>
        public static string INF002 {
            get {
                return ResourceManager.GetString("INF002", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a Transfer Completed.
        /// </summary>
        public static string INF003 {
            get {
                return ResourceManager.GetString("INF003", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a Hello {0}, you have received a transfer from {1} in the amount of R$ {2:F2}..
        /// </summary>
        public static string INF004 {
            get {
                return ResourceManager.GetString("INF004", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a Your transfer in the amount of {0} was successfully sent to {1}..
        /// </summary>
        public static string INF005 {
            get {
                return ResourceManager.GetString("INF005", resourceCulture);
            }
        }
    }
}
