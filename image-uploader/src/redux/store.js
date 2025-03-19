import { configureStore } from "@reduxjs/toolkit";
import imageReducer from "./reducers";  // Import the reducer handling image state

// Create the Redux store with imageReducer
const store = configureStore({
    reducer: imageReducer,
});

export default store;
