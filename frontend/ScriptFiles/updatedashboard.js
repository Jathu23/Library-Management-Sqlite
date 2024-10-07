let cads = document.getElementById("cardBox");
console.log( cads.children[1].children[0].children[0].innerHTML );

async function fetchBookCount() {
    try {
        let response = await fetch('https://localhost:7182/api/Book/CountTotalBooks');
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        let data = await response.json();
        return data;
    } catch (error) {
        console.error('Error fetching book count:', error);
        throw error; 
    }
}

async function updateDashboard() {
    try {
        const count = await fetchBookCount(); 
        cads.children[1].children[0].children[0].innerHTML = count; 
    } catch (error) {
        console.error('Error updating dashboard:', error); 
    }
}

updateDashboard();
