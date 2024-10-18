async function ValidateUser(nic, password) {
    try {
        let url = 'https://localhost:7182/api/User/ValidateUser?nic=' + encodeURIComponent(nic) + '&password=' + encodeURIComponent(password);

        let response = await fetch(url);

        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }

        let data = await response.json();
console.log(data);

        return data;
        
    } catch (error) {
        console.error("Error validating user:", error.message);
    }
}



document.getElementById("login").addEventListener("click", function() {
    document.getElementById("logincontainer").style.display = "none";
    document.getElementById("user-creation-page").style.display = "block";
});

document.getElementById("signup").addEventListener("click", function() {
    document.getElementById("user-creation-page").style.display = "none";
    document.getElementById("logincontainer").style.display = "block";
});

document.getElementById("loginForm").addEventListener("submit", async function (event) {
    event.preventDefault(); 

    const nicNumber = document.getElementById("loginNIC").value;
    const password = document.getElementById("loginPassword").value;
    const Urole = document.getElementById("LoginuserRole").value;
try {
    if (Urole == "member") {
        let validate = await ValidateUser(nicNumber,password);
        if (validate) {
            window.location.replace("../member/member.html"); 
            // window.location.replace ="../member/member.html"; 
        }else{
            document.getElementById("loginMessage").textContent = "User Id or Password incorrect";
        }
    }else{
        if (nicNumber == "admin" && password == "admin" ) {
            window.location.replace("../new-admin/dashboard-Admin .html");
        }else{
            document.getElementById("loginMessage").textContent = "User Id or Password incorrect";
        }
    }
 
} catch (error) {
    console.error('There was an error with the login:', error);
    document.getElementById("loginMessage").textContent = "An error occurred. Please try again.";
}


});








document.getElementById('creation-form').addEventListener('submit', async function (e) {
    e.preventDefault();

    const userData = {
        nic: document.getElementById('nicnumber').value,
        firstName: document.getElementById('firstname').value,
        lastName: document.getElementById('lastname').value,
        fullName: `${document.getElementById('firstname').value} ${document.getElementById('lastname').value}`,
        email: document.getElementById('email').value,
        password: document.getElementById('createPassword').value,
        phoneNumber: document.getElementById('phonenumber').value,
        joinDate: new Date().toISOString(), // Current date
        lastLoginDate: new Date().toISOString(),
        rentCount: 0, // Default value
        profileimg: document.getElementById('image').value 
    };
console.log(userData);

    // Validate password match
    if (userData.password !== document.getElementById('confirmpassword').value) {
        document.getElementById('createMessage').textContent = "Passwords do not match!";
        return;
    }

    try {
        const response = await fetch('https://localhost:7182/api/User/AddUser', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(userData),
        });

        if (response.ok) {
            document.getElementById('createMessage').textContent = 'User created successfully!';
        } else {
            const errorData = await response.json();
            document.getElementById('createMessage').textContent = `Error: ${errorData.message}`;
        }
    } catch (error) {
        document.getElementById('createMessage').textContent = `Error: ${error.message}`;
    }
});
