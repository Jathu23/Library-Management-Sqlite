document.getElementById("creation-form").addEventListener("submit", async function (event) {
    event.preventDefault(); // Prevent default form submission

    // Get form values
    const firstName = document.getElementById("firstname").value;
    const lastName = document.getElementById("lastname").value;
    const nicNumber = document.getElementById("nicnumber").value;
    const phoneNumber = document.getElementById("phonenumber").value;
    const email = document.getElementById("email").value;
    const createPassword = document.getElementById("createPassword").value;
    const confirmPassword = document.getElementById("confirmpassword").value;
    const userRole = document.getElementById("userRole").value;
    const ProfileImage=document.getElementById("image").value;

    // Check if passwords match
    if (createPassword !== confirmPassword) {
        document.getElementById("createMessage").innerText = "Passwords do not match. Please try again.";
        return; // Stop the form submission
    }

    // Generate a unique UserID (e.g., MBR01, MBR02, ...)
    const userCountResponse = await fetch("http://localhost:3000/users");
    const users = await userCountResponse.json();
   // Generate a unique UserID based on user role
   let userID;
   if (userRole === "admin") {
       userID = `ADM${String(users.filter(user => user.UserRole === "admin").length + 1).padStart(2, '0')}`; // For admin
   } else {
       userID = `MBR${String(users.filter(user => user.UserRole === "member").length + 1).padStart(2, '0')}`; // For members
   }


    // Get current date for JoinDate
    const joinDate = new Date().toISOString().split('T')[0]; // Format date as YYYY-MM-DD

    // Create user object
    const user = {
        UserID: userID,
        FirstName: firstName,
        LastName: lastName,
        NICNumber: nicNumber,
        PhoneNumber: phoneNumber,
        Email: email,
        JoinDate: joinDate,
        Password:confirmPassword,
        UserRole: userRole,
        RentCount: 0, // Default rent count
        ProfileImage: ProfileImage // Handle image uploads separately
    };

    try {
        // Send user data to the JSON server
        const response = await fetch("http://localhost:3000/users", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(user)
        });

        if (response.ok) {
            const result = await response.json();
            document.getElementById("createMessage").innerText = "User created successfully!";
            console.log(result);
        } else {
            document.getElementById("createMessage").innerText = "Error creating user.";
        }
    } catch (error) {
        document.getElementById("createMessage").innerText = "Network error.";
        console.error(error);
    }
});
