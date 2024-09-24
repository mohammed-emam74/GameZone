GameZone
GameZone is an online game store platform designed to provide a smooth and engaging experience for both administrators and users. Built with .NET Core for backend development and SQL Server for robust data management, GameZone enables seamless game browsing, user management, order processing, and role-based access control. The platform offers essential features like a shopping cart, news section for upcoming games, and customizable product management.
Technologies Used:
GameZone incorporates the following technologies and frameworks:
Backend Development: Built using .NET Core, providing high performance and scalability for modern web applications.
Database Management: SQL Server is used for efficient storage and retrieval of game data, orders, users, and news.
Authentication & Authorization: Role-based access control is implemented using ASP.NET Identity to manage user roles like Admin, Moderator, and User.
Shopping Cart: GameZone supports cart functionality, allowing users to add, remove, and reset items in their cart before placing orders.
Entity Framework Core: An ORM for interacting with the SQL Server database, making data handling efficient and code maintainable.
MVC Pattern: The project follows the MVC architectural pattern to separate concerns and provide clear structure.
ViewModels: Used to manage and validate data for forms and views, especially for game creation and editing.
Features:
*User and Role Management
Admin,User Roles: GameZone supports role-based access control, allowing for different levels of access. Admins can manage games, add news, and view detailed reports, while regular users can browse and purchase games.
Secure Authentication: ASP.NET Identity manages user authentication with role-based permissions.
*Game and Category Management
CRUD Operations: Administrators can create, read, update, and delete games,and news. Games can be associated with multiple categories and devices.
File Upload: Admins can upload cover images for games, ensuring that product pages are visually appealing.
*Shopping Cart
Add to Cart: Users can add games to their cart, view a cart summary, and proceed to place an order.
Reset Cart: A reset button allows users to clear their cart with a single click.
Order Management: Orders include details like shipping address, status, and items purchased, ensuring a smooth checkout process.
*News Section
Upcoming Games: The platform includes a news section where admins can add news about upcoming games, including details like the release date, platform, and description.
Viewable by All: Regular users can view the upcoming games section, keeping them informed about new releases.
