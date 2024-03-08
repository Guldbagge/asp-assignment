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


const switchMode = document.getElementById('switch-mode');
const body = document.body;

const isDarkMode = localStorage.getItem('darkMode') === 'true';

switchMode.checked = isDarkMode;

if (isDarkMode) {
    body.classList.add('dark-mode');
} else {
    body.classList.remove('dark-mode');
}

switchMode.addEventListener('change', toggleDarkMode);

function toggleDarkMode() {
    if (switchMode.checked) {
        body.classList.add('dark-mode');
        localStorage.setItem('darkMode', 'true'); 
    } else {
        body.classList.remove('dark-mode');
        localStorage.setItem('darkMode', 'false'); 
    }
}
