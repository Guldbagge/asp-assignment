document.addEventListener('DOMContentLoaded', function () {
    var menuButton = document.getElementById('mobil-menu-btn');
    var mobileMenu = document.getElementById('mobil-menu');

    if (menuButton && mobileMenu) {
        menuButton.addEventListener('click', function () {
            console.log('Knappen klickades');
            mobileMenu.classList.toggle('open');
            var ariaExpanded = mobileMenu.getAttribute('aria-expanded');
            mobileMenu.setAttribute('aria-expanded', ariaExpanded === 'true' ? 'false' : 'true');
        });
    } else {
        console.error('Knappen eller mobilmenyn kunde inte hittas.');
    }
});