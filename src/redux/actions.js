// Action to fetch images from the backend
export const fetchImages = () => async (dispatch) => {
    dispatch({ type: "FETCH_IMAGES_REQUEST" });

    try {
        const response = await fetch("http://localhost:5000/api/image");
        const data = await response.json();

        dispatch({ type: "FETCH_IMAGES_SUCCESS", payload: data }); // Store images in Redux state
    } catch (error) {
        dispatch({ type: "FETCH_IMAGES_FAILURE", payload: error.message }); // Store error
    }
};

// Action to upload an image
export const uploadImage = (file) => async (dispatch) => {
    dispatch({ type: "UPLOAD_IMAGE_REQUEST" });

    const formData = new FormData();
    formData.append("file", file);

    try {
        await fetch("http://localhost:5000/api/image/upload", {
            method: "POST",
            body: formData,
        });

        dispatch(fetchImages()); // Refresh image list after upload
    } catch (error) {
        dispatch({ type: "UPLOAD_IMAGE_FAILURE", payload: error.message });
    }
};
