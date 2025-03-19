import React, { useState } from "react";
import { useDispatch } from "react-redux";
import { uploadImage } from "../redux/actions";

const ImageUpload = () => {
    const [file, setFile] = useState(null); // State to hold the selected file
    const dispatch = useDispatch(); // Hook to dispatch Redux actions

    // Function to handle file selection
    const handleFileChange = (e) => {
        setFile(e.target.files[0]);
    };

    // Function to upload file to the backend
    const handleUpload = () => {
        if (file) {
            dispatch(uploadImage(file)); // Dispatch action to upload image
            setFile(null);
        } else {
            alert("Please select a file first.");
        }
    };

    return (
        <div className="upload-container">
            <input type="file" onChange={handleFileChange} />
            <button onClick={handleUpload}>Upload Image</button>
        </div>
    );
};

export default ImageUpload;
