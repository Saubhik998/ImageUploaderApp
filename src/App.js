import React from "react";
import { Provider } from "react-redux";
import store from "./redux/store";
import ImageUpload from "./components/ImageUpload";
import ImageList from "./components/ImageList";
import "./styles/styles.css";

const App = () => {
    return (
        <Provider store={store}>
            <div className="app-container">
                <h1>Image Uploader</h1>
                <ImageUpload />
                <ImageList />
            </div>
        </Provider>
    );
};

export default App;
