const destBtns = document.querySelectorAll(".classic-btn");
const grpBtns = document.querySelectorAll(".main-btn2");

destBtns.forEach((btn) => {
  btn.addEventListener("click", () => {
    btn.innerHTML = "<a>Going<a>";
  });
});

grpBtns.forEach((btn) => {
  btn.addEventListener("click", () => {
    btn.innerHTML = `<a><i
    class="icofont-check-circled"></i> Joined<a>`;
  });
});
