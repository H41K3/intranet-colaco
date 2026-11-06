const $ = (id) => document.getElementById(id);

const TIPO = { LINK: 0, DOWNLOAD: 1, SUBTITULO: 2 };

const lista = $("rows");
const caixaBusca = $("search-box");
const campoBusca = $("search");
const navInicio = document.querySelector(".nav-btn[data-home]");

document.querySelectorAll("[data-dep]").forEach((btn) =>
  btn.addEventListener("click", () => carregarSetor(btn.dataset.dep))
);
document.querySelectorAll("[data-home]").forEach((btn) =>
  btn.addEventListener("click", irInicio)
);
campoBusca.addEventListener("input", () => filtrar(campoBusca.value));

function el(tag, texto, classe) {
  const node = document.createElement(tag);
  if (texto) node.textContent = texto;
  if (classe) node.className = classe;
  return node;
}

function abrirView(viewId, navBtn) {
  document.querySelectorAll(".view").forEach((v) => v.classList.toggle("active", v.id === viewId));
  document.querySelectorAll(".nav-btn").forEach((b) => b.classList.toggle("active", b === navBtn));
  campoBusca.value = "";
}

function irInicio() {
  abrirView("view-dash", navInicio);
}

function filtrar(termo) {
  const texto = termo.toLowerCase();
  lista.querySelectorAll(".row, .subtab-header").forEach((item) => {
    item.style.display = item.textContent.toLowerCase().includes(texto) ? "" : "none";
  });
}

function mostrarMensagem(texto) {
  caixaBusca.hidden = true;
  lista.replaceChildren(el("p", texto, "muted"));
}

function criarSubtitulo(titulo) {
  return el("h3", titulo, "subtab-header");
}

function criarLinha({ titulo, url, type }) {
  const linha = el("a", "", "row");
  const info = el("div");
  info.append(el("h4", titulo));

  if (url) {
    info.append(el("small", url));
    Object.assign(linha, { href: url, target: "_blank", rel: "noopener noreferrer" });
  }

  linha.append(info, el("span", type === TIPO.DOWNLOAD ? "Baixar ↘" : "Abrir ↗"));
  return linha;
}

function criarItem(item) {
  return item.type === TIPO.SUBTITULO ? criarSubtitulo(item.titulo) : criarLinha(item);
}

async function carregarSetor(chave) {
  const navBtn = document.querySelector(`.nav-btn[data-dep="${chave}"]`);
  $("sector-title").textContent = navBtn.textContent;
  abrirView("view-sector", navBtn);
  mostrarMensagem("Carregando…");

  try {
    const resposta = await fetch(`/api/Setores/${chave}`);
    if (!resposta.ok) throw new Error(`HTTP ${resposta.status}`);
    const { links } = await resposta.json();

    if (links.length === 0) {
      mostrarMensagem("Ainda não existem portais ou ferramentas vinculadas a este setor.");
      return;
    }

    lista.classList.toggle("has-subtabs", links.some((l) => l.type === TIPO.SUBTITULO));
    lista.replaceChildren(...links.map(criarItem));
    caixaBusca.hidden = false;
  } catch {
    mostrarMensagem("Não foi possível carregar os recursos. Tente novamente.");
  }
}