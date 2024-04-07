import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';

const CourseDetails = () => {
    const params = useParams();
    const { courseId } = params;
    const [courseData, setCourseData] = useState(null);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await fetch(`http://localhost:5233/api/courses/${courseId}`);
                if (!response.ok) {
                    throw new Error('Failed to fetch course data');
                }
                const data = await response.json();
                setCourseData(data);
            } catch (error) {
                console.error('Error fetching course data:', error);
            }
        };

        fetchData();

    }, [courseId]); 

    return (
        <div>
            <h2>Course Details</h2>
            {courseData ? (
                <div>
                    <h3>{courseData.title}</h3>
                    <p>{courseData.content}</p>
                    
                </div>
            ) : (
                <p>Loading...</p>
            )}
        </div>
    );
};

export default CourseDetails;
