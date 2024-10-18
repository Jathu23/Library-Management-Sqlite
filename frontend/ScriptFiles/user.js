
async function showbooks_onUserpage() {
    try {
        const books = await  fetchbooks(true);
        books.forEach(book => {
            const card = document.createElement('div');
            card.classList.add('book-card');
            card.innerHTML = `
              <img src="https://localhost:7182/${book.images[0]}" alt="${book.title}">
              <h3>${book.title}</h3>
              <p>By ${book.author}</p>
              <p>${book.publishYear}</p>
            `;
            card.addEventListener('click', () => openModal(book));
            bookContainer.appendChild(card);
          });
    } catch (error) {
        console.error('Error showing books:',error)
    }
}
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


  
  const bookContainer = document.getElementById('book-container');
  const modal = document.getElementById('book-modal');
  const modalTitle = document.getElementById('modal-title');
  const modalAuthor = document.getElementById('modal-author');
  const modalGenre = document.getElementById('modal-genre');
  const modalPublishYear = document.getElementById('modal-publish-year');
  const modalCopies = document.getElementById('modal-copies');
  const modalRentCount = document.getElementById('modal-rent-count');
  const sliderImage = document.getElementById('slider-image');
  let currentImages = [];
  let currentIndex = 0;
  

  
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
    sliderImage.src =sliderImage.src = `https://localhost:7182/${currentImages[currentIndex]}`;
  }
  
  document.querySelector('.close').addEventListener('click', () => {
    modal.style.display = 'none';
  });
  
  document.getElementById('prev-btn').addEventListener('click', () => {
    currentIndex = (currentIndex > 0) ? currentIndex - 1 : currentImages.length - 1;
    sliderImage.src =sliderImage.src = `https://localhost:7182/${currentImages[currentIndex]}`;
  });
  
  document.getElementById('next-btn').addEventListener('click', () => {
    currentIndex = (currentIndex < currentImages.length - 1) ? currentIndex + 1 : 0;
    sliderImage.src =sliderImage.src = `https://localhost:7182/${currentImages[currentIndex]}`;
  });
  
  window.onclick = function(event) {
    if (event.target == modal) {
      modal.style.display = 'none';
    }
  }
  
  showbooks_onUserpage();

  sliderImage.src = 'https://localhost:7182/${currentImages[currentIndex]}';


