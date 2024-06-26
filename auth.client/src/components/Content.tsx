import React, { useEffect, useState } from 'react';
import { Link, Routes, Route } from 'react-router-dom';

interface Course {
    id: number;
    title: string;
    content: string;
}

const Content = () => {
    const [courses, setCourses] = useState<Course[]>([]);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await fetch('http://localhost:5233/api/courses/');
                if (!response.ok) {
                    throw new Error('Failed to fetch data');
                }
                const data = await response.json();
                setCourses(data);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };

        fetchData();
    }, []);

    const handleEdit = (id: number) => {
        console.log('Edit course with ID:', id);
    };

    const handleDelete = (id: number) => {
        console.log('Delete course with ID:', id);
    };
 

    return (
        <div>
            <h2>Courses
            </h2>
            <Link to='/courses/Add'><button type="button" className="btn btn-primary mb-2">
               + Add Course
            </button></Link>
            <table className="table">
                <thead>
                    <tr>
                        <th scope="col">ID</th>
                        <th scope="col">Title</th>
                        <th scope="col">Actions</th>
                    </tr>
                </thead>
                <tbody>
                    
                    {courses.map(course => (
                        <tr key={course.id}>
                            <td>{course.id}</td>
                            <td><Link to={`/courses/${course.id}`}>{ course.title }</Link></td>
                            <td>
                                <button
                                    type="button"
                                    className="btn btn-success mr-2"
                                    onClick={() => handleEdit(course.id)}
                                >
                                    Edit
                                </button>
                                <button
                                    type="button"
                                    className="btn btn-danger"
                                    onClick={() => handleDelete(course.id)}
                                >
                                    Delete
                                </button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default Content;
