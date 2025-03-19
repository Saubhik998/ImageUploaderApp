// Initial state of the application
const initialState = {
    images: [],     // List of uploaded images
    loading: false, // Loading state
    error: null,    // Error messages, if any
};

// Reducer function to manage different actions
const imageReducer = (state = initialState, action) => {
    switch (action.type) {
        case "FETCH_IMAGES_REQUEST":
            return { ...state, loading: true };  // Set loading state to true when fetching images

        case "FETCH_IMAGES_SUCCESS":
            return { ...state, loading: false, images: action.payload }; // Store fetched images

        case "FETCH_IMAGES_FAILURE":
            return { ...state, loading: false, error: action.payload };  // Store error if fetching fails

        case "UPLOAD_IMAGE_REQUEST":
            return { ...state, loading: true };  // Set loading state to true when uploading

        case "UPLOAD_IMAGE_FAILURE":
            return { ...state, loading: false, error: action.payload };  // Store error if upload fails

        default:
            return state;  // Return current state if no matching action type
    }
};

export default imageReducer;
