'use client'

import PageTitle from "@/components/pageTitle";
import { useCurrentOwner } from "@/hooks/useCurrentOwner";
import { useCurrentUser } from "@/hooks/useCurrentUser";
import { CredentialDetail } from "@/models";
import { useState } from "react";
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

type SchemaField = {
    label: string,
    type: string
}

export default function CreateCredentialForm() {
    const { currentUser } = useCurrentUser();
    const { token } = useCurrentOwner(currentUser);
    
    var defaultCredentialDetail : CredentialDetail = { 
        id: 0,
        name: '',
        description: '',
        ownerDid: '',
        schemaDefinition: '',
        createdAt: new Date(),
        updatedAt: new Date()
    };
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [newFieldLabel, setNewFieldLabel] = useState('');
    const [newFieldType, setNewFieldType] = useState('');
    const [fieldList, setFieldList] = useState<SchemaField[]>([]);

    var newSchemaField : SchemaField = {
        label: '',
        type: ''
    };

    var fieldTypeOptions = [
        'String',
        'Number',
        'Boolean'
    ];

    //this function validates new schema fields
    function validateNewField() {
        if(newFieldLabel === '' || newFieldType === '') {
            return false;
        }
        return true;
    }

    //this function adds a new field to the schema list
    function addField(e: React.MouseEvent<HTMLButtonElement, MouseEvent>) {
        e.preventDefault();

        if(!validateNewField()) {
            toast("All Schema Fields are Required", {
                position: "bottom-right",
                autoClose: 5000,
                hideProgressBar: false,
                closeOnClick: true,
                pauseOnHover: true,
                draggable: true,
                progress: undefined,
                type: "error",
                theme: "light",
              });
            return;
        }

        newSchemaField.label = newFieldLabel;
        newSchemaField.type = newFieldType;
        setFieldList([...fieldList, newSchemaField]);
    }

    //this function removes a field from the schema list
    function removeField(index: number) {
        fieldList.splice(index, 1);
        setFieldList([...fieldList]);
    }

    //this function returns a select option for each field type
    function renderFieldTypeOptions() {
        return fieldTypeOptions.map((fieldType, index) => {
            return (
                <option key={index} value={fieldType}>{fieldType}</option>
            )
        })
    }

    //this function iterates over the field list and returns a table row for each field
    function renderFieldList() {
        return fieldList.map((field, index) => {
            return (
                <tr key={index}>
                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                        {field.label}
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                        {field.type}
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap text-right text-sm font-medium">
                        <button type="button" className="text-indigo-600 hover:text-indigo-900" onClick={() => removeField(index)}>Remove</button>
                    </td>
                </tr>
            )
        })
    }

    function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
        event.stopPropagation();
        event.preventDefault();
        //credential.ownerDid = token!.unique_name;
    }

    return (
        <div className='container p-6 mx-auto my-3'>
            <PageTitle title='Create Credential' />
            <div className='card bg-base-100 shadow-xl mt-3'>
                <div className='card-body'>
                    <form className='form-control' onSubmit={handleSubmit}>
                        <label className='label'>
                            <span className='label-text'>Name</span>
                        </label>
                        <input type='text' placeholder='Name' className='input input-bordered' value={name} onChange={e => setName(e.target.value)} />
                        <label className='label'>
                            <span className='label-text'>Description</span>
                        </label>
                        <input type='text' placeholder='Description' className='input input-bordered' value={description} onChange={e => setDescription(e.target.value)} />
                        <label className='label'>
                            <span className='label-text'>Schema</span>
                        </label>
                        <div className="grid grid-cols-3 gap-2">
                            <div className="relative overflow-x-auto col-span-2">
                                <table className="w-full text-sm text-left">
                                    <thead className="text-xs uppercase">
                                        <tr>
                                            <th scope="col" className="px-6 py-3">
                                                Label
                                            </th>
                                            <th scope="col" className="px-6 py-3">
                                                Type
                                            </th>
                                            <th scope="col" className="px-6 py-3">
                                                <span className="sr-only">Edit</span>
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {renderFieldList()}
                                    </tbody>
                                </table>
                            </div>
                            <div className="card w-96 bg-base-100 shadow-xl py-3 px-6 w-auto">
                                <p className="text-xl font-bold leading-none tracking-tight">New Field</p>
                                <label className='label'>
                                    <span className='label-text'>Label</span>
                                </label>
                                <input type='text' placeholder='Label' className='input input-bordered' value={newFieldLabel} onChange={e => setNewFieldLabel(e.target.value)} />
                                <label className='label'>
                                    <span className='label-text'>Type</span>
                                </label>
                                <select className='select select-bordered w-full max-w-xs' value={newFieldType} onChange={e => setNewFieldType(e.target.value)}>
                                    <option value=''>Select Type</option>
                                    {renderFieldTypeOptions()}
                                </select>
                                <button className='btn btn-primary mt-3' onClick={e => addField(e)}>Add</button>
                            </div>
                        </div>
                        <div className='btn-group my-3'>
                            <button className='btn btn-primary'>Create</button>
                        </div>
                    </form>
                </div>
            </div>
            <ToastContainer />
        </div>
    );
}