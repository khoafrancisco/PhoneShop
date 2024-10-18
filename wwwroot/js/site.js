// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


{/* <script> */}
    function showPreview(imageUrl) {
        var carouselInner = document.querySelector('#productCarousel .carousel-inner');
        var activeItem = carouselInner.querySelector('.carousel-item.active');
        if (activeItem) {
            activeItem.classList.remove('active');
        }
        var newActiveItem = carouselInner.querySelector(`img[src$='${imageUrl}']`).parentElement;
        newActiveItem.classList.add('active');
    }

    function buyNow() {
        // Implement the logic to handle the "Buy Now" action
        window.location.href = '/checkout';
    }

    function addToCart() {
        // Implement the logic to handle the "Add to Cart" action
        alert('Product added to cart!');
    }
// </script>

document.addEventListener("DOMContentLoaded", function() {
    const images = document.querySelectorAll(".card-img-top");
    images.forEach(img => {
        img.addEventListener("mouseover", function() {
            this.style.transform = "scale(1.5)";
            this.style.transition = "transform 0.25s ease";
        });
        img.addEventListener("mouseout", function() {
            this.style.transform = "scale(1)";
        });
    });
});

