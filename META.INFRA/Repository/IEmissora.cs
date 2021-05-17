using META.DOMAIN;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace META.INFRA.Repository
{
    public interface IEmissora
    {

        public List<Emissoras> GetEmissora();

        public List<Emissoras> GetAudienciEmissora();

        public List<Emissoras> GetAudienciEmissoraPorVisao(Visao visao);
        public Emissoras GetOneEmissora(int id);
        public string EditarEmissora(Emissoras emissoras);

        public string CadastrarEmissora(Emissoras emissoras);
        public void DeletarEmissora(int id);
    }
}
