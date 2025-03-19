# **📷 Image Uploader App**

A **React.js & Redux** frontend with a **.NET Web API** backend that allows users to upload, view, and manage images efficiently.

## **🌟 Features**
- 📤 **Upload Images** – Select and upload images from your device.
- 🖼 **View Uploaded Images** – Displays all uploaded images in a gallery format.
- 🔄 **Real-time Updates** – Automatically fetches and displays newly uploaded images.
- 🛠 **Error Handling** – Provides fallbacks for broken images and server errors.
- 🚀 **Optimized State Management** – Uses Redux for efficient state handling.

---

## **📂 Project Structure**
```
ImageUploaderApp/
│── image-uploader/       # Frontend (React.js + Redux)
│   ├── src/
│   │   ├── components/    # UI Components (ImageList, ImageUpload)
│   │   ├── redux/         # Redux actions, reducers, and store
│   │   ├── styles.css     # Styling for UI components
│   │   ├── App.js         # Main application entry
│   │   ├── index.js       # React root file
│   ├── public/            # Static assets (favicon, index.html)
│   ├── package.json       # Dependencies and scripts
│── ImageUploaderAPI/      # Backend (.NET Web API)
│   ├── Controllers/       # API Controllers for handling requests
│   ├── Models/            # Data models for image storage
│   ├── Services/          # Business logic and database interactions
│   ├── Program.cs         # Entry point for .NET backend
│   ├── appsettings.json   # Configuration settings
```

---

## **🛠 Technologies Used**
### **Frontend (React)**
- ⚛️ **React.js** – Frontend framework
- 📦 **Redux Toolkit** – State management
- 🎨 **CSS** – Styling
- 🔗 **Axios / Fetch API** – For API requests

### **Backend (.NET Web API)**
- ⚙️ **ASP.NET Core Web API** – Backend framework
- 🗄 **MongoDB / SQL Server** – Database storage for images
- ☁️ **Cloudinary / Local Storage** – Image storage (optional)
- 🛡 **CORS & Authentication** – Security features

---

## **🚀 Getting Started**
### **1️⃣ Clone the Repository**
```sh
git clone https://github.com/Saubhik998/ImageUploaderApp.git
cd ImageUploaderApp
```

### **2️⃣ Frontend Setup**
```sh
cd image-uploader
npm install    # Install dependencies
npm start      # Start development server
```

### **3️⃣ Backend Setup**
```sh
cd ImageUploaderAPI
dotnet restore    # Install dependencies
dotnet run        # Start backend server
```

---

## **📌 API Endpoints**
### **Image Management**
| Method | Endpoint | Description |
|--------|---------|-------------|
| `GET` | `/api/image` | Fetch all uploaded images |
| `POST` | `/api/image/upload` | Upload a new image |

