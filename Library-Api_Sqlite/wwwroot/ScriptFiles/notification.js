async function fetchOverDueDetails() {
    try {
        let response = await fetch('https://localhost:7182/api/Lent/findoverdueAll');
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        let data = await response.json();
        console.log(data);
        return data;

    } catch (error) {
        console.error('Error fetching users:', error);
        throw error;
    }
}

async function showOverduedatails() {
    try {
        const users = await fetchOverDueDetails();
        console.log(users);
        
        let userTable = document.querySelector('#notification-table tbody');
        userTable.innerHTML = ''; // Clear previous table rows

        users.forEach(user => {
            userTable.innerHTML += `
            <tr>
                <td>${user.usernic}</td>
                <td>${user.isbn}</td>
                <td>${user.lentid}</td>
                <td>${user.copies}</td>
                <td>${new Date(user.lentDate).toLocaleDateString()}</td>
                <td>${new Date(user.returnDate).toLocaleDateString()}</td>
                <td>${user.status}</td>
            </tr>
            `;
        });

    } catch (error) {
        console.error('Error showing users:', error);
    }
}

showOverduedatails();
