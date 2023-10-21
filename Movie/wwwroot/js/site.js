$('[data-open-modal]').click(async function () {
    event.preventDefault();
    console.log('daa');

    let url = $(this).attr('href');
    let response = await fetch(url);
    let result = await response.text();

    console.log(result);

    $('.modal-body').html(result);

    console.log(url);

    $('#exampleModal').modal('show');
});

let page;
let totalPages;
let url;

function initPagination(p, t, u) {
    page = p;
    totalPages = t;
    url = u;
}

let isScroll = true;

$(window).scroll(async function () {

    if ($(window).scrollTop() + $(window).height() > $(document).height() - 100 && isScroll) {
        isScroll = false;

        console.log(page);
        if (page < totalPages) {
            page++;
            let response = await fetch(`${url}&page=${page}`);
            let result = await response.text();

            $('.myList').append(result);
        }

        if (page >= totalPages) {
            $('#buttonNext').remove();
        }

        isScroll = true;
    }
});