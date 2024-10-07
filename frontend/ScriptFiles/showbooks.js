async function fetchbooks() {
    try {
        let response = await fetch('https://localhost:7182/api/Book/GetAllbook');
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        let data = await response.json();
        console.log(data);
        return data;
       
        

    } catch (error) {
        console.error('Error fetching books:', error);
        throw error; 
    }
}

async function showbooks_onAdminpage() {
    try {
        const books = await  fetchbooks();
        let table = document.getElementById('bookinv_table');
        table.innerHTML = ` <thead>
          <tr>
            <td>ISBN</td>
            <td>Title</td>
            <td>Author</td>
            <td>Publish Year</td>
            <td>Add Date</td>
            <td>Copies</td>
            <td>Genre</td>
            <td>Available Copies</td>
            <td>Actions</td>   
          </tr>
        </thead>`;
        books.forEach(book => {
            table.innerHTML += `    <tbody>
      <tr>${book.isbn}</tr>
      <tr>${book.title}</tr>
      <tr>${book.author}</tr>
      <tr>${book.publishYear}</tr>
      <tr>${book.addDateTime}</tr>
      <tr>${book.copies}</tr>
      <tr>${book.genre}</tr>
      <tr>${book.aviCopies}</tr>
      <tr><button>Edit</button> <button>Delete</button></tr>
    </tbody>`;
          

            
        });
    } catch (error) {
        console.error('Error showing books:',error)
    }
}
async function showbooks_onUserpage() {
    try {
        const books = await  fetchbooks();
       
        books.forEach(book => {
            console.log(book.title);
            
        });
    } catch (error) {
        console.error('Error showing books:',error)
    }
}


showbooks_onAdminpage();