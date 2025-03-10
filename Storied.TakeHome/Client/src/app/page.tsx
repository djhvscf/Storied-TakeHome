'use client'
import Image from "next/image";
import { useState, useEffect } from 'react';
import DropdownSelect from '../components/DropdownSelect';

export default function Home() {
    const [options, setOptions] = useState([]);

    useEffect(() => {
        fetch('http://localhost:5063/api/people/all')
            .then(response => response.json())
            .then(data => setOptions(data))
            .catch(error => console.error('Error fetching options:', error));
    }, []);

    return (
        <div className="flex items-center justify-center h-screen bg-gray-100">
            <div className="p-8 bg-white rounded shadow">
                <h1>Dropdown Select Component</h1>
                <DropdownSelect
                    label="Select a person"
                    options={options}
                    onChange={(value: any) => console.log("Selected value:", value)}
                />
            </div>
        </div>
    );
}
