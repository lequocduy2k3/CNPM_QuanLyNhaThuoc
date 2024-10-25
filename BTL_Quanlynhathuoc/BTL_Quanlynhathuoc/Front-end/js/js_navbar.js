document.addEventListener('DOMContentLoaded', function () {
    const hamburger = document.querySelector('.hamburger');
    const mobileMenu = document.querySelector('.mobile-menu');
    const mobileDropdown = document.querySelector('.mobile-dropdown');


    if (hamburger) {
        hamburger.addEventListener('click', function () {
            mobileMenu.classList.toggle('active');
        });
    } else {
    }

    if (mobileDropdown) {
        mobileDropdown.addEventListener('click', function (e) {
            e.preventDefault();
            this.querySelector('.mobile-submenu').classList.toggle('active');
        });
    } else {
    }
});