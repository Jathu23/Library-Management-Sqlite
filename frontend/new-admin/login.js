// document.getElementById('loginForm').addEventListener('submit', function(event) {
//   event.preventDefault();

//   // Get user inputs
//   const nicNumber = document.getElementById('nicnumber').value;
//   const password = document.getElementById('loginPassword').value;
//   const userRole = document.getElementById('userRole').value;

//   // Get users array from local storage
//   const admins = JSON.parse(localStorage.getItem('admins')) || [];
//   const members = JSON.parse(localStorage.getItem('members')) || [];
//   let validUser = null;

//   // Check if the user is an admin or member
//   if (userRole === 'admin') {
//       validUser = admins.find(user => user.nicnumber === nicNumber && user.password === password);
//   } else if (userRole === 'member') {
//       validUser = members.find(user => user.nicnumber === nicNumber && user.password === password);
//   }

//   if (validUser) {
//       // Store NIC number in session storage
//       sessionStorage.setItem('loggedinUser', JSON.stringify({ nicnumber: nicNumber }));

//       // Redirect based on user role
//       if (userRole === 'admin') {
//         alert('welcome admin')
//           window.location.href = '../new-admin/dashboard-Admin .html'; // Replace with your admin page URL
//       } else if (userRole === 'member') {
//         alert("hello member")
//           window.location.href = '../member/member.html'; // Replace with your member page URL
//       }
//   } else {
//       document.getElementById('loginMessage').innerText = 'Invalid NIC number or password!';
//   }
// });