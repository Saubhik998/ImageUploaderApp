import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { fetchImages } from "../redux/actions";

const ImageList = () => {
    const dispatch = useDispatch(); // Hook to dispatch Redux actions
    const { images, loading, error } = useSelector((state) => state); // Access Redux state

    useEffect(() => {
        dispatch(fetchImages()); // Fetch images when the component mounts
    }, [dispatch]);

    if (loading) return <p>Loading...</p>;
    if (error) return <p>Error: {error}</p>;
    if (!images || images.length === 0) return <p>No images found.</p>;

    return (
        <div className="gallery">
            {images.map((image) => {
                const imageUrl = `http://localhost:5000/api/image/${image._id}`;

                return (
                    <div key={image._id} className="image-card">
                        <img
                            src={imageUrl}
                            alt={image.filename}
                            className="uploaded-image"
                            onError={(e) => {
                                e.target.src = "https://via.placeholder.com/150";
                            }}
                        />
                        <p>{image.filename}</p>
                    </div>
                );
            })}
        </div>
    );
};

export default ImageList;
