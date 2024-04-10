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
    select();
    searchQuery();
});

function select() {
    let selectContainer = document.querySelector('.select');
    let selected = selectContainer.querySelector('.selected');
    let selectOptions = selectContainer.querySelector('.select-options');

    try {
        selectContainer.addEventListener('click', function () {
            selectOptions.style.display = (selectOptions.style.display === 'block' ? 'none' : 'block');
        });

        let options = selectOptions.querySelectorAll('.option');
        options.forEach(function (option) {
            option.addEventListener('click', function () {
                selected.innerHTML = this.textContent;
                selectOptions.style.display = 'none';
                let category = this.getAttribute('data-value');
                selected.setAttribute('data-value', category);
                upddateCourseByFilters();
            });
        });

    } catch (error) {
        console.error(error);
    }
}

function searchQuery() {
    try
    {
        document.querySelector('#searchQuery').addEventListener('keyup', function () {
            upddateCourseByFilters()
        })

    }
    catch
    {
        console.error(error);
    }
}

function upddateCourseByFilters() {

    const category = document.querySelector('.select .selected').getAttribute('data-value') || 'all';
    const searchQuery = document.querySelector('#searchQuery').value

    const url = (`/courses/index?category=${encodeURIComponent(category)}&searchQuery=${encodeURIComponent(searchQuery)}`);

        
    fetch(url)
    .then(res => res.text())
        .then(data => {
            const parser = new DOMParser();
            const dom = parser.parseFromString(data, 'text/html');
            document.querySelector('.grid').innerHTML = dom.querySelector('.grid').innerHTML;

            const pagination = dom.querySelector('.pagination') ? dom.querySelector('.pagination').innerHTML : ''
            document.querySelector('.pagination').innerHTML = pagination;
        })
        .catch(err => console.error(err));
}

document.addEventListener('DOMContentLoaded', function () {
    handleProfileImageUpload();
})

function handleProfileImageUpload() {
    try {
        let fileUploader = document.querySelector('#fileUploader');
        if (fileUploader != undefined) {
            fileUploader.addEventListener('change', function () {
                if (this.files.length > 0) {
                    this.form.submit();
                }
            })
        }

    }
    catch (error) {
        console.error(error);
    }
}



//document.addEventListener('DOMContentLoaded', function () {
//    const dropdownBtn = document.querySelector('.dropbtn');
//    const courseSelect = document.getElementById('courseSearchInput');
//    const courseSearchButton = document.getElementById('courseSearchButton');
//    const filterDropdown = document.querySelector('.dropdown-content');

//    dropdownBtn.addEventListener('click', function () {
//        this.nextElementSibling.classList.toggle('show');
//    });

//    window.DomContentLoaded = function () {
//        const urlParams = new URLSearchParams(window.location.search);
//        const category = urlParams.get('category');

//        if (category !== null) {
//            CategorySelect.value = category;
//        }
//    };

//    dropdownBtn.addEventListener('change', function () {
//        localStorage.setItem('selectedCategory', this.value);
//    });

//    const savedCategory = localStorage.getItem('selectedCategory');
//    if (savedCategory !== null) {
//        CategorySelect.value = savedCategory;
//    }

//    courseSearchButton.addEventListener('click', searchCourses);
//    courseSelect.addEventListener('keypress', function (event) {
//        if (event.key === 'Enter') {
//            searchCourses();
//        }
//    });

//    function searchCourses() {
//        const searchTerm = courseSelect.value.trim().toLowerCase();
//        const courseCards = document.querySelectorAll('.course-card');

//        courseCards.forEach(function (card) {
//            const title = card.querySelector('.course-card_body h2').innerText.toLowerCase();
//            const author = card.querySelector('.course-card_body p').innerText.toLowerCase();

//            if (title.includes(searchTerm) || author.includes(searchTerm)) {
//                card.style.display = 'block'; // Visa kurskortet om söktermen matchar titeln eller författaren
//            } else {
//                card.style.display = 'none'; // Dölj kurskortet om söktermen inte matchar titeln eller författaren
//            }
//        });
//    }



//    filterDropdown.addEventListener('click', function (event) {
//        const filterOption = event.target.dataset.filter;
//        if (filterOption) {
//            filterCourses(filterOption);
//        }
//    });

//    function filterCourses(filterOption) {
//        const courseCards = document.querySelectorAll('.course-card');
//        courseCards.forEach(function (card) {
//            if (filterOption === 'bestsellers' && card.querySelector('.bestseller')) {
//                card.style.display = 'block';
//            } else if (filterOption === 'non-bestsellers' && !card.querySelector('.bestseller')) {
//                card.style.display = 'block';
//            } else if (filterOption === 'all') {
//                card.style.display = 'block';
//            } else {
//                card.style.display = 'none';
//            }
//        });
//    }
//});
