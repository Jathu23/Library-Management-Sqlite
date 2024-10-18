// document.getElementById("loginForm").addEventListener("submit", async function (event) {
//     event.preventDefault(); // Prevent default form submission




    // const nicNumber = document.getElementById("nicnumber").value.trim();
    // const loginPassword = document.getElementById("loginPassword").value.trim();
    // const userRole = document.getElementById("userRole").value;

    // if (!nicNumber || !loginPassword) {
    //     document.getElementById("loginMessage").innerText = "Please fill in all fields.";
    //     return;
    // }

//     try {
//         const response = await fetch(`https://localhost:7182/api/User/ValidateUser?nic=1001&password=password123`);
//         if (!response.ok) throw new Error("Failed to fetch users.");
// console.log(response);

//         const users = await response.json();
//                 console.log(users);

//         const user = users.find(user => 
//             user.NICNumber === nicNumber && user.Password === loginPassword 
//             // && user.UserRole === userRole
//         );
//         console.log(user);
        

//         if (user) {
//             localStorage.setItem("loggedInUser", JSON.stringify(user));

//             if (userRole === "member") {
//                 window.location.href = "../member/member.html";
//             } else if (userRole === "admin") {
//                 window.location.href = "../new-admin/dashboard-Admin.html"; // Removed extra space
//             }
//         } else {
//             document.getElementById("loginMessage").innerText = "Invalid login details. Please try again.";
//         }
//     } catch (error) {
//         document.getElementById("loginMessage").innerText = "Network error. Please try again later.";
//         console.error(error);
//     }
// });

// document.getElementById("creation-form").addEventListener("submit", async function (event) {
//     event.preventDefault(); // Prevent default form submission

//     const firstName = document.getElementById("firstname").value;
//     const lastName = document.getElementById("lastname").value;
//     const nicNumber = document.getElementById("nicnumber").value;
//     const phoneNumber = document.getElementById("phonenumber").value;
//     const email = document.getElementById("email").value;
//     const createPassword = document.getElementById("createPassword").value;
//     const confirmPassword = document.getElementById("confirmpassword").value;
//     const userRole = document.getElementById("userRole").value;
//     const ProfileImage = document.getElementById("image").value;

//     if (createPassword !== confirmPassword) {
//         document.getElementById("createMessage").innerText = "Passwords do not match. Please try again.";
//         return;
//     }

//     const userCountResponse = await fetch("http://localhost:3000/users");
//     const users = await userCountResponse.json();
    
//     let userID;
//     if (userRole === "admin") {
//         userID = `ADM${String(users.filter(user => user.UserRole === "admin").length + 1).padStart(2, '0')}`;
//     } else {
//         userID = `MBR${String(users.filter(user => user.UserRole === "member").length + 1).padStart(2, '0')}`;
//     }

//     const joinDate = new Date().toISOString().split('T')[0];

//     const user = {
//         UserID: userID,
//         FirstName: firstName,
//         LastName: lastName,
//         NICNumber: nicNumber,
//         PhoneNumber: phoneNumber,
//         Email: email,
//         JoinDate: joinDate,
//         Password: confirmPassword,
       
//         RentCount: 0,
//         ProfileImage: ProfileImage
//     };

//     try {
//         const response = await fetch("http://localhost:3000/users", {
//             method: "POST",
//             headers: {
//                 "Content-Type": "application/json"
//             },
//             body: JSON.stringify(user)
//         });

//         if (response.ok) {
//             const result = await response.json();
//             document.getElementById("createMessage").innerText = "User created successfully!";
//             console.log(result);
//         } else {
//             document.getElementById("createMessage").innerText = "Error creating user.";
//         }
//     } catch (error) {
//         document.getElementById("createMessage").innerText = "Network error.";
//         console.error(error);
//     }
// });

document.getElementById("login").addEventListener("click", function() {
    document.getElementById("logincontainer").style.display = "none";
    document.getElementById("user-creation-page").style.display = "block";
});

document.getElementById("signup").addEventListener("click", function() {
    document.getElementById("user-creation-page").style.display = "none";
    document.getElementById("logincontainer").style.display = "block";
});

document.getElementById("loginForm").addEventListener("submit", async function (event) {
    event.preventDefault(); // Prevent form from submitting normally

    const nicNumber = document.getElementById("nicnumber").value;
    const password = document.getElementById("loginPassword").value;

    // Construct the API URL with the NIC and password dynamically
    const apiUrl = `https://localhost:7182/api/User/ValidateUser?nic=${nicNumber}&password=${password}`;

    try {
        // Fetch request to validate user credentials
        const response = await fetch(apiUrl);

        if (!response.ok) {
            throw new Error('Network response was not ok');
        }

        const data = await response.json(); // Parse the response to JSON
       
        const userstatus=data.message
        console.log(userstatus);
        
        
            // Check the user role from the response and redirect accordingly
            if (userstatus) {
                alert("skjaghsuhao")// Assuming `role` is returned in the response
                window.location.href = "../member/member.html"; // Redirect to admin dashboard
            } else (userstatus === "User is  valid")
             {
                window.location.href = "../new-member/dashboard-Member.html"; // Redirect to member dashboard
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
        profileimg: document.getElementById('image').value || 'default.jpg', // Fallback image
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
