document.addEventListener("DOMContentLoaded", function () {
    const pagerLinks = document.querySelectorAll(".page-link");

    pagerLinks.forEach(link => {
        link.addEventListener("click", function (event) {
            event.preventDefault(); // Предотвращаем переход по ссылке
            const url = this.href;

            // Отправляем AJAX-запрос
            fetch(url, { headers: { "X-Requested-With": "XMLHttpRequest" } })
                .then(response => {
                    if (!response.ok) {
                        throw new Error("Ошибка загрузки данных");
                    }
                    return response.text();
                })
                .then(html => {
                    // Обновляем только блок с продуктами
                    document.querySelector("#product-list").innerHTML = html;
                })
                .catch(error => console.error("Ошибка:", error));
        });
    });
});
