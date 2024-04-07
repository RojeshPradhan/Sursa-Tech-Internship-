import React, { SyntheticEvent, useState } from "react";
import { Navigate } from 'react-router-dom';

const AddCourse = () => {
    const [title, setTitle] = useState('');
    const [content, setContent] = useState('');
    const [redirect, setRedirect] = useState(false);

    const submit = async (e: SyntheticEvent) => {
        e.preventDefault();

        await fetch('http://localhost:5233/api/courses/', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                title,
                content
            })
        });

        setRedirect(true);
    }

    if (redirect) {
        return <Navigate to="/" />;
    }

    return (
        <form onSubmit={submit}>
            <h1 className="h3 mb-3 fw-normal">Add New Course</h1>

            <input className="form-control" placeholder="Title" required
                onChange={e => setTitle(e.target.value)}
            />

            <textarea className="form-control" placeholder="Content" required
                onChange={e => setContent(e.target.value)}
            />

            <button className="w-100 btn btn-lg btn-primary" type="submit">Submit</button>
        </form>
    );
};

export default AddCourse;
