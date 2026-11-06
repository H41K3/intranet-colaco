namespace IntranetColaco.Api.Models;

// O nome "Type" e os valores numéricos fazem parte do contrato com o app.js (constante TIPO).
public enum TipoLink
{
    Link = 0,
    Download = 1,
    Subtitulo = 2
}

public sealed record LinkItem(string Titulo, string? Url = null, TipoLink Type = TipoLink.Link);
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
                Link("Cloudfy", "https://contadigital.cloudfy.net.br/login"),
                Link("Jettax", "https://admin.jettax360.com.br/"),
                Link("Santander", "https://www.santander.com.br/"),
                Link("Sicredi", "https://www.sicredi.com.br/home/"),
                Link("Trello", "https://id.atlassian.com/login"),
                Link("VMpay", "https://vmpay.vertitecnologia.com.br/g/users/sign_in"),
                Link("Xpay", "https://br.x-pay.app/login"),

                Subtitulo("Download"),
                Download("Itaú", "https://guardiao.itau.com.br/UpdateServer/aplicativoitau.msi")
            ]),

            ["fiscal"] = new("Setor Fiscal",
            [
                Subtitulo("Web"),
                Link("Econet", "https://www.econeteditora.com.br/"),
                Link("Emissor Nacional de NFS-e", "https://www.nfse.gov.br/EmissorNacional/Login"),
                Link("Jettax", "https://admin.jettax360.com.br/"),
                Link("Receita Estadual do Paraná", "https://receita.pr.gov.br/login"),
                Link("Receita Federal — e-CAC", "https://cav.receita.fazenda.gov.br/autenticacao/login"),

                Subtitulo("Download"),
                Download("SEDIF", "https://www.fazenda.mg.gov.br/export/sites/fazenda/empresas/declaracoes_demonstrativos/DeSTDA_SEDIF-SN/files/SEDIF010925.zip"),
                Download("SPED Contribuições", "https://servicos.receita.fazenda.gov.br/publico/programas/SpedPisCofinsPVA/EFD-Contribuicoes_w64-6.2.0.exe"),
                Download("SPED Fiscal ICMS/IPI", "https://servicos.receita.fazenda.gov.br/publico/programas/Sped/SpedFiscal/SpedEFD_w64-6.1.1.exe"),

                Subtitulo("Prefeituras"),
                Link("Campo Largo - Autoatendimento IPM", "https://www.campolargo.pr.gov.br/autoatendimento/"),
                Link("Curitiba - ISS", "https://isscuritiba.curitiba.pr.gov.br/iss/default.aspx"),
                Link("Pinhais - Autoatendimento IPM", "https://nfse-pinhais.atende.net/autoatendimento/servicos/nfse?redirected=1"),
                Link("São José dos Pinhais - ISS", "https://nfe.sjp.pr.gov.br/servicos/issOnline2/")
            ]),

            ["processos"] = new("Setor de Processos", []),

            ["sistemas-gerais"] = new("Sistemas Gerais",
            [
                Subtitulo("Web"),
                Link("Bitrix24", "https://www.bitrix24.net/portal/list/"),
                Link("Hub de Atendimento", "https://colaco.smartdata.app.br/"),
                Link("Onvio", "https://onvio.com.br/"),
                Link("RHiD", "https://www.rhid.com.br/v2/#/login"),

                Subtitulo("Download"),
                Download("Domínio", "https://cfdownload.dominiosistemas.com.br/instalacao/contabil/106a10/Instala.exe"),
                Download("Sittax Token/ Bytoken Certificados", "https://drive.google.com/file/d/1kQOEdFayoMHVKVMAs_WDpQjKAW9P73a7/view")
            ])
        };

    private static LinkItem Subtitulo(string titulo) => new(titulo, Type: TipoLink.Subtitulo);
    private static LinkItem Link(string titulo, string url) => new(titulo, url);
    private static LinkItem Download(string titulo, string url) => new(titulo, url, TipoLink.Download);
}