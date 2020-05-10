using System;
using System.Collections.Generic;

namespace APIAulaYoutube
{
    public class Apresentacao
    {
        public Apresentacao()
        {
            rotas = new List<string>();
            rotas.Add("/clientes");
        }
        private List<string> rotas { get; set; }

        public string Mensagem { get { return "Seja bem vindo a nossa API"; } }
        public List<string> Rotas { get { return this.rotas; } }

        public Validacao ValidarLogin(string usuario, string senha)
        {
            if(usuario == "Danilo" && senha == "123456")
            {
                return new Validacao() { Sucesso = true };
            }
            else
            {
                return new Validacao() { Sucesso = false };
            }
        }
    }
}
