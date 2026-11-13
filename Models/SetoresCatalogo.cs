namespace IntranetColaco.Api.Models;

public enum TipoLink
{
    Link = 0,
    Download = 1,
    Subtitulo = 2,
    Pasta = 3
}

public sealed record LinkItem(string Titulo, string? Url = null, TipoLink Type = TipoLink.Link, LinkItem[]? SubLinks = null);
public sealed record SetorInfo(string Nome, LinkItem[] Links);

public static class SetoresCatalogo
{
    public static readonly IReadOnlyDictionary<string, SetorInfo> Todos =
        new Dictionary<string, SetorInfo>(StringComparer.OrdinalIgnoreCase)
        {
            ["contabil"] = new("Setor Contábil", []),

            ["financeiro"] = new("Setor Financeiro",
            [
                Subtitulo("Web"),
                Pasta("Bancos e Pagamentos",
                [
                    Link("Santander", "https://www.santander.com.br/"),
                    Link("Sicredi", "https://www.sicredi.com.br/home/"),
                    Link("VMpay", "https://vmpay.vertitecnologia.com.br/g/users/sign_in"),
                    Link("Xpay", "https://br.x-pay.app/login")
                ]),
                Pasta("Gestão e Operações",
                [
                    Link("Cloudfy", "https://contadigital.cloudfy.net.br/login"),
                    Link("Jettax", "https://admin.jettax360.com.br/"),
                    Link("Trello", "https://id.atlassian.com/login")
                ]),
                Subtitulo("Download"),
                Download("Itaú", "https://guardiao.itau.com.br/UpdateServer/aplicativoitau.msi")
            ]),

            ["fiscal"] = new("Setor Fiscal",
            [
                Subtitulo("Web"),
                Pasta("Portais Estaduais e Federais",
                [
                    Link("Emissor Nacional de NFS-e", "https://www.nfse.gov.br/EmissorNacional/Login"),
                    Link("Receita Estadual do Paraná", "https://receita.pr.gov.br/login"),
                    Link("Receita Federal — e-CAC", "https://cav.receita.fazenda.gov.br/autenticacao/login")
                ]),
                Pasta("Prefeituras",
                [
                    Link("Campo Largo - Autoatendimento IPM", "https://www.campolargo.pr.gov.br/autoatendimento/"),
                    Link("Curitiba - ISS", "https://isscuritiba.curitiba.pr.gov.br/iss/default.aspx"),
                    Link("Pinhais - Autoatendimento IPM", "https://nfse-pinhais.atende.net/autoatendimento/servicos/nfse?redirected=1"),
                    Link("São José dos Pinhais - ISS", "https://nfe.sjp.pr.gov.br/servicos/issOnline2/")
                ]),
                Pasta("Sistemas de Consultoria e Gestão",
                [
                    Link("Econet", "https://www.econeteditora.com.br/"),
                    Link("Jettax", "https://admin.jettax360.com.br/")
                ]),
                Subtitulo("Download"),
                Download("SEDIF", "https://www.fazenda.mg.gov.br/export/sites/fazenda/empresas/declaracoes_demonstrativos/DeSTDA_SEDIF-SN/files/SEDIF010925.zip"),
                Download("SPED Contribuições", "https://servicos.receita.fazenda.gov.br/publico/programas/SpedPisCofinsPVA/EFD-Contribuicoes_w64-6.2.0.exe"),
                Download("SPED Fiscal ICMS/IPI", "https://servicos.receita.fazenda.gov.br/publico/programas/Sped/SpedFiscal/SpedEFD_w64-6.1.1.exe")
            ]),

            ["pessoal"] = new("Setor Pessoal",
            [
                Subtitulo("Web"),
                Pasta("Governo e Empregador",
                [
                    Link("Conectividade Social", "https://conectividadesocialv2.caixa.gov.br/sicns/"),
                    Link("Consulta Empréstimos", "https://servicos.mte.gov.br/empregador/#/login"),
                    Link("FGTS Digital", "https://fgtsdigital.sistema.gov.br/portal/login"),
                    Link("Plataforma Facilita MTE", "https://www.gov.br/trabalho-e-emprego/pt-br/servicos/facilita"),
                    Link("Seguro Desemprego", "https://sd.maisemprego.mte.gov.br/sdweb/empregadorweb/index.jsf")
                ]),
                Pasta("Sistemas de Gestão e Folha",
                [
                    Link("Melius", "https://portal.melius.software/"),
                    Link("Onvio", "https://cav.receita.fazenda.gov.br/autenticacao")
                ]),
                Pasta("Certificação e Autenticação",
                [
                    Link("eCac", "https://cav.receita.fazenda.gov.br/autenticacao"),
                    Link("Vínculo CNPJ/Certificado", "https://servicos.acesso.gov.br/")
                ]),
                Subtitulo("Download"),
                Link("Nenhum download disponível no momento.", null)
            ]),

            ["processos"] = new("Setor de Processos",
            [
                Subtitulo("Web"),
                Pasta("Certidões Negativas de Débitos (CND)",
                [
                    Link("CND Estadual Mato Grosso do Sul", "https://servicos.efazenda.ms.gov.br/pndfis/home/emissao"),
                    Link("CND Estadual Paraná", "https://cdwfazenda.paas.pr.gov.br/cdwportal/certidao/automatica"),
                    Link("CND FGTS", "https://consulta-crf.caixa.gov.br/consultacrf/pages/consultaEmpregador.jsf"),
                    Link("CND Municipal Belo Horizonte", "https://cnd.pbh.gov.br/CNDOnline/"),
                    Link("CND Municipal Campo Grande", "https://siatportal.campogrande.ms.gov.br/servicos/cidadao/certidao"),
                    Link("CND Municipal Curitiba", "https://cnd-cidadao.curitiba.pr.gov.br/Certidao/SolicitarCnpj"),
                    Link("CND Trabalhista", "https://cndt-certidao.tst.jus.br/gerarCertidao"),
                    Link("Regularize", "https://www.regularize.pgfn.gov.br/")
                ]),
                Pasta("Juntas Comerciais",
                [
                    Link("Empresa Fácil MA (Maranhão)", "https://www.empresafacil.ma.gov.br/home/"),
                    Link("Empresa Fácil PR (Paraná)", "https://www.empresafacil.pr.gov.br/"),
                    Link("JUCEA (Amazonas)", "https://www.jucea.am.gov.br/"),
                    Link("JUCEC (Ceará)", "https://www.ce.gov.br/jucec/"),
                    Link("JUCEG (Goiás)", "https://goias.gov.br/juceg/"),
                    Link("JUCEMAT (Mato Grosso)", "https://www.jucemat.mt.gov.br/"),
                    Link("JUCEMG (Minas Gerais)", "https://jucemg.mg.gov.br/"),
                    Link("JUCEMS (Mato Grosso do Sul)", "https://www.jucems.ms.gov.br/"),
                    Link("JUCEP (Paraíba)", "https://jucep.pb.gov.br/"),
                    Link("JUCER (Rondônia)", "https://rondonia.ro.gov.br/jucer/"),
                    Link("JUCERN (Rio Grande do Norte)", "https://www.jucern.rn.gov.br/"),
                    Link("JUCESC (Santa Catarina)", "https://www.jucesc.sc.gov.br/"),
                    Link("JUCESP (São Paulo)", "https://www.jucesponline.sp.gov.br/"),
                    Link("JUCETINS (Tocantins)", "https://www.to.gov.br/jucetins"),
                    Link("JUCIS-DF (Distrito Federal)", "https://jucis.df.gov.br/"),
                    Link("JUCISRS (Rio Grande do Sul)", "https://jucisrs.rs.gov.br/inicial"),
                    Link("Piauí Digital (Piauí)", "https://www.piauidigital.pi.gov.br/home/junta-comercial/"),
                    Link("Redesim", "https://www.gov.br/empresas-e-negocios/pt-br/redesim"),
                    Link("VRE Redesim (SP)", "https://vreredesim.sp.gov.br/home")
                ]),
                Pasta("Licenciamentos e Registos",
                [
                    Link("ANVISA", "https://www.gov.br/anvisa/pt-br/setorregulado/autorizacao-de-funcionamento-afe-ou-ae"),
                    Link("Cadastur", "https://cadastur.turismo.gov.br/hotsite/#!/public/capa/entrar#capaInicio"),
                    Link("CEASA PR", "https://www.ceasa.pr.gov.br/servicos/Servicos/Meio-Ambiente/Solicitar-licenciamento-ambiental-9OoqbNG0"),
                    Link("Corpo de Bombeiros PR", "https://www.bombeiros.pr.gov.br/Pagina/Renovacao-de-Licenciamento"),
                    Link("DEAM - Polícia Civil PR", "https://www.policiacivil.pr.gov.br/Pagina/Explosivos-Armas-e-Municoes"),
                    Link("DPCAP - Polícia Civil PR", "https://www.policiacivil.pr.gov.br/Editoria/DPCAP"),
                    Link("Licenciamento Ambiental (Curitiba)", "https://sima.curitiba.pr.gov.br/"),
                    Link("LISA Curitiba", "https://servicodigital.curitiba.pr.gov.br/servicos-digitais+licenciamento-sanitario+5ec440a6c4e3d801af2e79a3"),
                    Link("PROCEC Curitiba", "https://procec.curitiba.pr.gov.br/Home/Index/7069OCEC"),
                    Link("URBS", "https://www.urbs.curitiba.pr.gov.br/portal/")
                ]),
                Pasta("Trabalho e Empregador",
                [
                    Link("Conectividade Social ICP V2", "https://conectividadesocialv2.caixa.gov.br/procuracao/#/"),
                    Link("Domicílio Eletrônico Trabalhista (DET)", "https://det.sit.trabalho.gov.br/login?r=%2Fservicos"),
                    Link("FGTS Digital", "https://fgtsdigital.sistema.gov.br/")
                ]),
                Pasta("Tributário e Prefeituras",
                [
                    Link("DEC SP", "https://www.dec.fazenda.sp.gov.br/DEC/UCLogin/login.aspx"),
                    Link("Econet", "https://www.econeteditora.com.br/"),
                    Link("ISS Curitiba", "https://isscuritiba.curitiba.pr.gov.br/iss/default.aspx"),
                    Link("ISS São José dos Pinhais", "https://nfe.sjp.pr.gov.br/servicos/issOnline2/index.php"),
                    Link("Prefeitura de Campo Largo", "https://campolargo.atende.net/autoatendimento/servicos"),
                    Link("Receita Estadual PR", "https://receita.pr.gov.br/login"),
                    Link("Receita Federal (e-CAC)", "https://cav.receita.fazenda.gov.br/autenticacao/login"),
                    Link("Simples Nacional", "https://www8.receita.fazenda.gov.br/simplesnacional/")
                ]),
                Subtitulo("Download"),
                Link("Nenhum download disponível no momento.", null)
            ]),

            ["sistemas-gerais"] = new("Sistemas Gerais",
            [
                Subtitulo("Web"),
                Pasta("Gestão e Comunicação",
                [
                    Link("Bitrix24", "https://www.bitrix24.net/portal/list/"),
                    Link("Flash", "https://hros.flashapp.com.br/authentication/login"),
                    Link("Hub de Atendimento", "https://colaco.smartdata.app.br/"),
                    Link("Onvio", "https://onvio.com.br/"),
                    Link("RHiD", "https://www.rhid.com.br/v2/#/login")
                ]),
                Subtitulo("Download"),
                Download("Domínio", "https://cfdownload.dominiosistemas.com.br/instalacao/contabil/106a10/Instala.exe"),
                Download("ExperIP", "https://experip.com.br/app/ExperIP-3.21.3002_Cloud_X.exe"),
                Download("Sittax Token/ Bytoken Certificados", "https://drive.google.com/file/d/1kQOEdFayoMHVKVMAs_WDpQjKAW9P73a7/view")
            ])
        };

    private static LinkItem Subtitulo(string titulo) => new(titulo, Type: TipoLink.Subtitulo);
    private static LinkItem Link(string titulo, string? url) => new(titulo, url);
    private static LinkItem Download(string titulo, string url) => new(titulo, url, TipoLink.Download);
    private static LinkItem Pasta(string titulo, LinkItem[] subLinks) => new(titulo, null, TipoLink.Pasta, subLinks);
}