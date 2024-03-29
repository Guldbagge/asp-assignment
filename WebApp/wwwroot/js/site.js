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

document.addEventListener('DOMContentLoaded', function () {
    let sw = document.querySelector('#switch-mode');

    sw.addEventListener('change', function () {
        let theme = this.checked ? 'dark' : 'light';

        fetch(`/sitesettings/changetheme?mode=${theme}`)
        .then(res => {
                if (res.ok) {
                    window.location.reload();
                } else {
                    console.error('Theme change failed');
                }
        })

    })

})

document.addEventListener('DOMContentLoaded', function () {
    const dropdownBtn = document.querySelector('.dropbtn');
    const courseSelect = document.getElementById('courseSearchInput');
    const courseSearchButton = document.getElementById('courseSearchButton');
    const filterDropdown = document.querySelector('.dropdown-content');

    dropdownBtn.addEventListener('click', function () {
        this.nextElementSibling.classList.toggle('show');
    });

    window.DomContentLoaded = function () {
        const urlParams = new URLSearchParams(window.location.search);
        const category = urlParams.get('category');

        if (category !== null) {
            CategorySelect.value = category;
        }
    };

    dropdownBtn.addEventListener('change', function () {
        localStorage.setItem('selectedCategory', this.value);
    });

    const savedCategory = localStorage.getItem('selectedCategory');
    if (savedCategory !== null) {
        CategorySelect.value = savedCategory;
    }

    courseSearchButton.addEventListener('click', searchCourses);
    courseSelect.addEventListener('keypress', function (event) {
        if (event.key === 'Enter') {
            searchCourses();
        }
    });

    function searchCourses() {
        const searchTerm = courseSelect.value.trim().toLowerCase();
        const courseCards = document.querySelectorAll('.course-card');

        courseCards.forEach(function (card) {
            const title = card.querySelector('.course-card_body h2').innerText.toLowerCase();
            const author = card.querySelector('.course-card_body p').innerText.toLowerCase();

            if (title.includes(searchTerm) || author.includes(searchTerm)) {
                card.style.display = 'block'; // Visa kurskortet om söktermen matchar titeln eller författaren
            } else {
                card.style.display = 'none'; // Dölj kurskortet om söktermen inte matchar titeln eller författaren
            }
        });
    }



    filterDropdown.addEventListener('click', function (event) {
        const filterOption = event.target.dataset.filter;
        if (filterOption) {
            filterCourses(filterOption);
        }
    });

    function filterCourses(filterOption) {
        const courseCards = document.querySelectorAll('.course-card');
        courseCards.forEach(function (card) {
            if (filterOption === 'bestsellers' && card.querySelector('.bestseller')) {
                card.style.display = 'block';
            } else if (filterOption === 'non-bestsellers' && !card.querySelector('.bestseller')) {
                card.style.display = 'block';
            } else if (filterOption === 'all') {
                card.style.display = 'block';
            } else {
                card.style.display = 'none';
            }
        });
    }
});




//document.addEventListener('DOMContentLoaded', function () {
//    const CategorySelect = document.getElementById('CategorySelect');

//    CategorySelect.addEventListener('change', function () {
//        this.form.submit();

//    });

//    window.DomContentLoaded = function () {
//        const urlParams = new URLSearchParams(window.location.search);
//        const category = urlParams.get('category');

//        if (category !== null) {
//            categorySelect.value = category;
//        }
//    };

//    categorySelect.addEventListener('change', function () {
//        localStorage.setItem('selectedCategory', this.value);

//        });

//    const savedCategory = localStorage.getItem('selectedCategory');
//    if (savedCategory !== null) {
//        categorySelect.value = savedCategory;
//    }

 
//const switchMode = document.getElementById('switch-mode');
//const body = document.body;

//const isDarkMode = localStorage.getItem('darkMode') === 'true';

//switchMode.checked = isDarkMode;

//if (isDarkMode) {
//    body.classList.add('dark-mode');
//} else {
//    body.classList.remove('dark-mode');
//}

//switchMode.addEventListener('change', toggleDarkMode);

//function toggleDarkMode() {
//    if (switchMode.checked) {
//        body.classList.add('dark-mode');
//        localStorage.setItem('darkMode', 'true'); 
//    } else {
//        body.classList.remove('dark-mode');
//        localStorage.setItem('darkMode', 'false'); 
//    }
//}
