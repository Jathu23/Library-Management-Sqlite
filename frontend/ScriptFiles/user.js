
const bookContainer = document.getElementById('book-container');
const modal = document.getElementById('book-modal');
const modalTitle = document.getElementById('modal-title');
const modalAuthor = document.getElementById('modal-author');
const modalGenre = document.getElementById('modal-genre');
const modalPublishYear = document.getElementById('modal-publish-year');
const modalCopies = document.getElementById('modal-copies');
const modalRentCount = document.getElementById('modal-rent-count');
const sliderImage = document.getElementById('slider-image');


const genreFilter = document.getElementById('genreFilter');
const authorFilter = document.getElementById('authorFilter');
const yearFilter = document.getElementById('yearFilter');
let inputbar = document.getElementById('booksearchinput');


let currentImages = [];
let currentIndex = 0;


async function fetchbooks(ordervalue) {
  try {
    let url = 'https://localhost:7182/api/Book/GetOrderedByPublishYear?ascending=' + encodeURIComponent(ordervalue);
    let response = await fetch(url);
    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }
    let data = await response.json();

    return data;

  } catch (error) {
    console.error("Error fetching books:", error.message);
  }

}
async function fetchSearchBook(input) {
  try {
    let url = 'https://localhost:7182/api/Book/SearchByTitle?title=' + encodeURIComponent(input);
    let response = await fetch(url);
    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }
    let data = await response.json();
    return data;

  } catch (error) {
    console.error("An error occurred:", error.message);
  }
}
async function fetchpublishyears() {
  try {
    let response = await fetch('https://localhost:7182/api/Book/GetAllPublishYears');
    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }
    let data = await response.json();

    return data;
  } catch (error) {
    console.error("An error occurred:", error.message);
  }
}
async function fetchGenres() {
  try {
    let response = await fetch('https://localhost:7182/api/Book/GetAllGenres');
    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }
    let data = await response.json();

    return data;
  } catch (error) {
    console.error("An error occurred:", error.message);
  }
}
async function fetchAuthors() {
  try {
    let response = await fetch('https://localhost:7182/api/Book/GetAllAuthors');
    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }
    let data = await response.json();

    return data;
  } catch (error) {
    console.error("An error occurred:", error.message);
  }
}
async function fetchCategorizebooks(genre, author, publishYear) {

  let url = 'https://localhost:7182/api/Book/Categorization?';



  if (genre) {
    url += `genre=${encodeURIComponent(genre)}&`;
    console.log(genre);

  }
  if (author) {
    url += `author=${encodeURIComponent(author)}&`;
    console.log(author);

  }
  if (publishYear) {
    url += `publishYear=${encodeURIComponent(publishYear)}&`;
    console.log(publishYear);

  }

  try {
    const response = await fetch(url, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json'
      }
    });

    if (!response.ok) {
      throw new Error(`Error: ${response.status} - ${response.statusText}`);
    }

    const books = await response.json();

    return books;

  } catch (error) {
    console.error('Error fetching books:', error);
  }
}
async function Add_dropdown_options() {
  try {
    const genres = await fetchGenres();
    const authors = await fetchAuthors();
    const years = await fetchpublishyears();

    createDropdown(genreFilter, genres);
    createDropdown(authorFilter, authors);
    createDropdown(yearFilter, years);
  } catch (error) {

  }
}
function createDropdown(dropdown, options) {
  options.forEach(option => {
    const opt = document.createElement('option');
    opt.value = option;
    opt.textContent = option;
    dropdown.appendChild(opt);
  });
}
async function filterbooks() {
  let selectGener = genreFilter.value;
  let selectAuthor = authorFilter.value;
  let selectYear = yearFilter.value;

  try {
    let books = await fetchCategorizebooks(selectGener, selectAuthor, selectYear);
    await displaybooks(books);

  } catch (error) {
    console.log(error.message);
  }

}
async function showAllbooks() {
  const books = await fetchbooks(true);
  
  await displaybooks(books);
}
async function showsearchbooks(input) {
  try {
      let books = await fetchSearchBook(input);
      await displaybooks(books);
  } catch (error) {
    console.error("An error occurred fetchSearchBook:", error.message);
  }
}
async function displaybooks(bookArray) {
  try {
    bookContainer.innerHTML = ``;
    bookArray.forEach(book => {
      const card = document.createElement('div');
      card.classList.add('book-card');
      card.innerHTML = `
            <div class="image_div">
             <img src="https://localhost:7182/${book.images[0]}" alt="${book.title}">
            </div>
              <h3>${book.title}</h3>
              <p>By ${book.author}</p>
              <p>${book.publishYear}</p>
            `;
      card.addEventListener('click', () => openModal(book));
      bookContainer.appendChild(card);
    });
  } catch (error) {
    console.error('Error showing books:', error)
  }
}
function openModal(book) {
  modal.style.display = 'block';
  modalTitle.innerText = book.title;
  modalAuthor.innerText = `Author: ${book.author}`;
  modalGenre.innerText = `Genre: ${book.genre.join(', ')}`;
  modalPublishYear.innerText = `Published: ${book.publishYear}`;
  modalCopies.innerText = `Copies: ${book.copies} (Available: ${book.aviCopies})`;
  modalRentCount.innerText = `Rented: ${book.rentCount} times`;
  currentImages = book.images;
  currentIndex = 0;
  sliderImage.src = sliderImage.src = `https://localhost:7182/${currentImages[currentIndex]}`;
}


Add_dropdown_options();
showAllbooks();

inputbar.addEventListener('input', () => {
  let input = inputbar.value;
  if (!input) {
    showAllbooks();
  } else {
     showsearchbooks(inputbar.value);
  }
});

genreFilter.addEventListener('change', () => {
  filterbooks();
});
authorFilter.addEventListener('change', () => {
  filterbooks();
});
yearFilter.addEventListener('change', () => {
  filterbooks();
});



document.querySelector('.close').addEventListener('click', () => {
  modal.style.display = 'none';
});

document.getElementById('prev-btn').addEventListener('click', () => {
  currentIndex = (currentIndex > 0) ? currentIndex - 1 : currentImages.length - 1;
 sliderImage.src = `https://localhost:7182/${currentImages[currentIndex]}`;
});

document.getElementById('next-btn').addEventListener('click', () => {
  currentIndex = (currentIndex < currentImages.length - 1) ? currentIndex + 1 : 0;
sliderImage.src = `https://localhost:7182/${currentImages[currentIndex]}`;
});

window.onclick = function (event) {
  if (event.target == modal) {
    modal.style.display = 'none';
  }
}

showbooks_onUserpage();

sliderImage.src = 'https://localhost:7182/${currentImages[currentIndex]}';


