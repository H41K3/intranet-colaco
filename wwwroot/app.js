const $ = (id) => document.getElementById(id);
const TIPO = { LINK: 0, DOWNLOAD: 1, SUBTITULO: 2, PASTA: 3 };
const lista = $("rows");
const caixaBusca = $("search-box");
const campoBusca = $("search");
const navInicio = document.querySelector(".nav-btn[data-home]");

document
  .querySelectorAll("[data-dep]")
  .forEach((btn) =>
    btn.addEventListener("click", () => carregarSetor(btn.dataset.dep)),
  );
document
  .querySelectorAll("[data-home]")
  .forEach((btn) => btn.addEventListener("click", irInicio));
if (campoBusca)
  campoBusca.addEventListener("input", () => filtrar(campoBusca.value));

function el(tag, texto, classe) {
  const node = document.createElement(tag);
  if (texto) node.textContent = texto;
  if (classe) node.className = classe;
  return node;
}

function abrirView(viewId, navBtn) {
  document
    .querySelectorAll(".view")
    .forEach((v) => v.classList.toggle("active", v.id === viewId));
  document
    .querySelectorAll(".nav-btn")
    .forEach((b) => b.classList.toggle("active", b === navBtn));
  if (campoBusca) campoBusca.value = "";
}

function irInicio() {
  if (navInicio) abrirView("view-dash", navInicio);
}

function filtrar(termo) {
  const texto = termo.toLowerCase();
  lista
    .querySelectorAll(".row, .subtab-header, .folder-container, p.muted")
    .forEach((item) => {
      const matches = item.textContent.toLowerCase().includes(texto);
      item.style.display = matches ? "" : "none";
      if (matches && item.classList.contains("folder-container")) {
        item
          .querySelectorAll(".row, p.muted")
          .forEach((r) => (r.style.display = ""));
      }
    });
}

function mostrarMensagem(texto) {
  if (caixaBusca) caixaBusca.hidden = true;
  lista.replaceChildren(el("p", texto, "muted"));
}

function criarSubtitulo(titulo) {
  return el("h3", titulo, "subtab-header");
}

function criarLinha(item) {
  if (!item.url) {
    return el("p", item.titulo, "muted");
  }
  const linha = el("a", "", "row");
  const info = el("div");
  info.append(el("h4", item.titulo));
  info.append(el("small", item.url));
  Object.assign(linha, {
    href: item.url,
    target: "_blank",
    rel: "noopener noreferrer",
  });
  linha.append(
    info,
    el("span", item.type === TIPO.DOWNLOAD ? "Baixar ↘" : "Abrir ↗"),
  );
  return linha;
}

function criarPasta(item) {
  const container = el("div", "", "folder-container");
  const btn = el("button", item.titulo, "acc-line-btn folder-btn");
  btn.type = "button";
  const panel = el("div", "", "acc-panel folder-panel");
  if (item.subLinks) {
    item.subLinks.forEach((sub) => panel.append(criarLinha(sub)));
  }
  btn.addEventListener("click", () => {
    const isOpen = btn.classList.contains("active");
    document.querySelectorAll(".folder-btn").forEach((b) => {
      b.classList.remove("active");
      b.nextElementSibling.style.maxHeight = null;
      b.nextElementSibling.classList.remove("open");
    });
    if (!isOpen) {
      btn.classList.add("active");
      panel.classList.add("open");
      panel.style.maxHeight = panel.scrollHeight + "px";
    }
  });
  container.append(btn, panel);
  return container;
}

function criarItem(item) {
  if (item.type === TIPO.SUBTITULO) return criarSubtitulo(item.titulo);
  if (item.type === TIPO.PASTA) return criarPasta(item);
  return criarLinha(item);
}

async function carregarSetor(chave) {
  const navBtn = document.querySelector(`.nav-btn[data-dep="${chave}"]`);
  if ($("sector-title")) $("sector-title").textContent = navBtn.textContent;
  abrirView("view-sector", navBtn);
  mostrarMensagem("Carregando…");
  try {
    const resposta = await fetch(`/api/Setores/${chave}`);
    if (!resposta.ok) throw new Error();
    const data = await resposta.json();
    const links = data.links || [];
    if (links.length === 0) {
      mostrarMensagem(
        "Ainda não existem portais ou ferramentas vinculadas a este setor.",
      );
      return;
    }
    lista.classList.toggle(
      "has-subtabs",
      links.some((l) => l.type === TIPO.SUBTITULO),
    );
    lista.replaceChildren(...links.map(criarItem));
    if (caixaBusca) caixaBusca.hidden = false;
  } catch {
    mostrarMensagem("Não foi possível carregar os recursos. Tente novamente.");
  }
}
