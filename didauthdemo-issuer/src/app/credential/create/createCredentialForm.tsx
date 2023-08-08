'use client'

import PageTitle from "@/components/pageTitle";
import { useCurrentOwner } from "@/hooks/useCurrentOwner";
import { useCurrentUser } from "@/hooks/useCurrentUser";
import { CredentialClaimSchema, CredentialDetail } from "@/models";
import { useRouter } from "next/navigation";
import { useState } from "react";
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

export default function CreateCredentialForm({ credentialDetail }: { credentialDetail: CredentialDetail }) {
    const router = useRouter()
    const { currentUser } = useCurrentUser();
    const { token } = useCurrentOwner(currentUser);
    
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [newFieldLabel, setNewFieldLabel] = useState('');
    const [newFieldType, setNewFieldType] = useState('');
    const [fieldList, setFieldList] = useState<CredentialClaimSchema[]>([]);

    var newSchemaField : CredentialClaimSchema = {
        label: '',
        type: '',
        claim: ''
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
        setNewFieldLabel('');
        setNewFieldType('');
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

    async function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
        event.stopPropagation();
        event.preventDefault();

        if(name === '' || description === '') {
            toast("Name and Description are Required", {
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

        const credential: CredentialDetail = { 
            id: credentialDetail.id,
            name: name,
            description: description,
            ownerDid: credentialDetail.ownerDid === '' ? token!.unique_name : credentialDetail.ownerDid,
            schemaDefinition: fieldList,
            createdAt: credentialDetail.createdAt,
            updatedAt: credentialDetail.updatedAt
        };

        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(credential)
        };

        const resp = await fetch('https://localhost:7243/api/create-credentials', requestOptions);
        const data = await resp.json();
        router.push(`/credential/${data.id}`);
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
                                <table className="table">
                                    <thead className="text-xs uppercase bg-base-200">
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
                            <div className="card bg-base-200 py-3 px-6 w-auto">
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