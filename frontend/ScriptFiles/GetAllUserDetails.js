async function getUserDetails() {
    try {
        let response = await fetch('http://localhost:5102/api/User/GetAll');
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

async function showUserdatails() {
    try {
        const users = await getUserDetails();
        let userTable = document.getElementById('bookTableBody');
        userTable.innerHTML = '';

        users.forEach(user => {
            userTable.innerHTML += `
            <tr>
                <td>${user.nic}</td>
                <td>${user.firstName}</td>
                <td>${user.lastName}</td>
                <td>${user.fullName}</td>
                <td>${user.email}</td>
                <td>${user.password}</td>
                <td>${user.phoneNumber}</td>
                <td>${new Date(user.joinDate).toLocaleDateString()}</td>
                <td>${new Date(user.lastLoginDate).toLocaleDateString()}</td>
                <td>${user.rentCount}</td>
                <td><img src="${user.profileimg}" alt="${user.fullName} Image" style="width:50px;height:50px;"></td>
            </tr>
            `;
        });

    } catch (error) {
        console.error('Error showing users:', error);
    }
}

showUserdatails();
