const booksInLibrary = JSON.parse(localStorage.getItem('books')) || [];
let bookNumber = document.getElementById("totalBooks");
bookNumber.textContent=booksInLibrary.length;

const lotallendtings = JSON.parse(localStorage.getItem(''))