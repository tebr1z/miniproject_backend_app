$(document).ready(function () {
	//Search
	$(document).on("keyup", "#searchInput", function () {
		let searchValue = $(this).val().trim();
		if (searchValue.trim() === "") {
			$("#searchResults").html("");
			return;
		}
		axios.get("/product/searchProduct", { params: { searchTerm: searchValue } })
			.then(response => {
				$("#searchResults").html(response.data);
			})
			.catch(error => {
				console.error(error);
			});
	});
	//Modal
    $(".productModal").click(function (ev) {
        ev.preventDefault();

        let url = $(this).attr("href");

        axios.get(url)
            .then(function (response) {
               /* console.log(response.data);*/
				$(".modal-body").html(response.data);

				// prodct details slider active
				$('.product-large-slider').slick({
					fade: true,
					arrows: false,
					asNavFor: '.pro-nav'
				});


				// product details slider nav active
				$('.pro-nav').slick({
					slidesToShow: 4,
					asNavFor: '.product-large-slider',
					arrows: false,
					focusOnSelect: true
				});

				// testimonial carousel active js
				$('.testimonial-active').slick({
					dots: true,
					arrows: false,
					responsive: [
						{
							breakpoint: 992,
							settings: {
								dots: false
							}
						}
					]
				});


				// image zoom effect
				$('.img-zoom').zoom();


				// quantity change js
				$('.pro-qty').prepend('<span class="dec qtybtn">-</span>');
				$('.pro-qty').append('<span class="inc qtybtn">+</span>');
				$('.qtybtn').on('click', function () {
					var $button = $(this);
					var oldValue = $button.parent().find('input').val();
					if ($button.hasClass('inc')) {
						var newVal = parseFloat(oldValue) + 1;
					} else {
						// Don't allow decrementing below zero
						if (oldValue > 0) {
							var newVal = parseFloat(oldValue) - 1;
						} else {
							newVal = 0;
						}
					}
					$button.parent().find('input').val(newVal);
				});


            })
            
    })

})

