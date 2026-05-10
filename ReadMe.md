📱 Most Wanted App
📌 Overview
The Most Wanted application is a mobile app built with .NET MAUI to help track and report wanted persons. It allows users to view wanted individuals, report sightings with GPS and media, and upload evidence. The app is designed with an offline-first strategy using SQLite for local storage and synchronizes with a remote API when online.

🏛️ Architecture
Pattern: MVVM (Model–View–ViewModel)

Layers:

Views (XAML): UI pages (MainPage, AddPerson, ReportSpottedPage, WantedPersonsDetailPage).

ViewModels: Presentation logic (WantedPersonViewModel, SpottedViewModel, AddWantedViewModel, UploadViewModel).

Services: Business logic (WantedPersonService, WantedPersonServiceOnline, SpottedService, AddWantedServices, ServiceSelector).

Database: SQLite/MySQL for local storage, REST API for online sync.

🎨 Features
View list of wanted persons (offline & online).

Add new wanted persons with details and images.

Report sightings with GPS coordinates and media.

Upload files linked to sightings or wanted records.

Offline-first design with automatic sync when online.

🗄️ Database Schema
Wanted_Person: ID, Name, Crime Type, Notes, Date_Added, Last_Updated.

Spotted: ID, Wanted_ID, Notes, Latitude, Longitude, Media_Path, Media_Type, Timestamp.

Upload: ID, Wanted_ID, File_Path, File_Type, Uploaded_At.

⚙️ Development Plan (Waterfall SDLC)
Requirements → Define features, schema, user stories.

Design → MVVM architecture, wireframes, UX system.

Implementation → Build Views, ViewModels, Services.

Testing → Unit, integration, UI, sync validation.

Deployment → Package for Android, publish to Play Store.

Maintenance → Monitor, add notifications, scale features.

🖥️ System Requirements
📱 Client (Mobile App)
OS: Android 10+

Framework: .NET MAUI

Storage: 200 MB free space

RAM: 2 GB minimum

Permissions: Location, Camera, Media, Internet

🖥️ Server / API
OS: Windows Server 2019+ or Linux (RHEL recommended)

Database: MySQL 8.x / SQLite (local testing)

Runtime: .NET 8 SDK & Runtime

Web Server: IIS / Apache / Nginx

Security: HTTPS, JWT/OAuth2 authentication

🛠️ Development Environment
IDE: Visual Studio 2022 (MAUI workload)

SDKs: .NET 8, Android SDK

Tools: MySQL Workbench, Postman, Git

🚀 Setup & Installation
Clone the repository:

bash
git clone https://github.com/imanisterling-svg/MostWanted.git
Install dependencies:

bash
dotnet restore
Run the app:

bash
dotnet build
dotnet run
📈 Future Enhancements
Push notifications for nearby sightings.

Map integration for real-time tracking.

Role-based access (citizens, police).

Analytics dashboard for crime trends.

👤 Author
Developed by Imani Sterling  
Location: Saint Catherine, Jamaica