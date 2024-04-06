import React from "react";
import Content from "../components/Content";


const Home = (props: { name: string }) => {
    const token = localStorage.getItem("token");

    return (
        <div>
            {token ? 'Welcome!' : props.name ? 'Hi ' + props.name : 'You are not logged in'}
            <Content/>

            
        </div>
    );
};

export default Home;
