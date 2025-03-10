import { useState, useRef, useEffect } from 'react';

const DropdownSelect = ({ label, options, onChange }) => {
    const [isOpen, setIsOpen] = useState(false);
    const [searchTerm, setSearchTerm] = useState('');
    const [selected, setSelected] = useState(null);
    const wrapperRef = useRef(null);

    useEffect(() => {
        const handleClickOutside = (event) => {
            if (wrapperRef.current && !wrapperRef.current.contains(event.target)) {
                setIsOpen(false);
            }
        };
        document.addEventListener('mousedown', handleClickOutside);
        return () =>
            document.removeEventListener('mousedown', handleClickOutside);
    }, []);

    const filteredOptions = options.filter(option =>
        option.label.toLowerCase().includes(searchTerm.toLowerCase())
    );

    const handleOptionClick = (option) => {
        setSelected(option);
        setIsOpen(false);
        onChange(option.label);
    };

    return (
        <div ref={wrapperRef} className="relative w-full max-w-[500px] font-sans">
            <div
                onClick={() => setIsOpen(prev => !prev)}
                className="flex justify-between items-center p-2.5 bg-white border border-gray-300 rounded cursor-pointer transition duration-200 hover:border-gray-400 focus-within:border-blue-600 focus-within:shadow-outline"
            >
                <span>{selected ? selected.label : label}</span>
                <svg
                    className={`w-4 h-4 transform transition-transform duration-200 ${isOpen ? 'rotate-180' : 'rotate-0'}`}
                    xmlns="http://www.w3.org/2000/svg"
                    fill="none"
                    viewBox="0 0 24 24"
                    stroke="currentColor"
                >
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M19 9l-7 7-7-7" />
                </svg>
            </div>

            {isOpen && (
                <div className="absolute top-full left-0 mt-1 w-full max-h-[300px] bg-white border border-gray-200 rounded shadow-lg z-10 overflow-hidden">
                    {/* Search Input */}
                    <input
                        type="text"
                        placeholder="Search..."
                        value={searchTerm}
                        onChange={(e) => setSearchTerm(e.target.value)}
                        className="w-full p-2 border border-gray-300 rounded text-base focus:outline-none focus:border-blue-600"
                    />
                    {/* Options List */}
                    <ul className="m-0 p-0 list-none max-h-[240px] overflow-y-auto">
                        {filteredOptions.length > 0 ? (
                            filteredOptions.map((option, index) => (
                                <li
                                    key={option.value || index}
                                    onClick={() => handleOptionClick(option)}
                                    className={`p-2.5 cursor-pointer transition-colors duration-200 option-hover ${
                                        selected && selected.label === option.label ? 'selected-value font-medium' : ''
                                    }`}
                                >
                                    {option.label}
                                </li>
                            ))
                        ) : (
                            <li key="no-options" className="p-2.5 text-gray-500 italic">
                                No options found
                            </li>
                        )}
                    </ul>
                </div>
            )}
        </div>
    );
};

export default DropdownSelect;
