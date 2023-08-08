'use client'

import { useCurrentOwner } from "@/hooks/useCurrentOwner";
import { useCurrentUser } from "@/hooks/useCurrentUser";
import { CredentialDetail } from "@/models";
import { useEffect, useState } from "react";

export type CredentialDetailSummaryProps = {
    id: string
}

export default function CredentialDetailSummary({id}: CredentialDetailSummaryProps) {
    var defaultCredentialDetail: CredentialDetail = {
        id: 0,
        name: '',
        description: '',
        ownerDid: '',
        schemaDefinition: [],
        createdAt: new Date(),
        updatedAt: new Date()
    };
    const [ credential, setCredential] = useState(defaultCredentialDetail);
    const { currentUser } = useCurrentUser();
    const { token } = useCurrentOwner(currentUser);

    //onload this function fires    
    useEffect(() => {
        if(token === null) return;

        fetch(`http://localhost:5196/api/get-credential-schemas/${token.unique_name}/details/${id}`, { cache: 'no-store'})
        .then(res => {
            if(res.ok) {
                res.json().then(resObj => {
                    setCredential(resObj);
                });
            }
        });
    }, [token]);

    
    return (
        <div className="grid grid-cols-2 gap-4 card bg-white shadow-lg p-3 my-3">
            <div>
                <h3 className="mb-2 text-md font-bold leading-none tracking-tight">Name</h3>
                <p className="leading-none tracking-tight">{credential.name}</p>
            </div>
            <div>
                <h3 className="mb-2 text-md font-bold leading-none tracking-tight">Created At</h3>
                <p className="leading-none tracking-tight">{credential.createdAt.toLocaleString()}</p>
            </div>
            <div>
                <h3 className="mb-2 text-md font-bold leading-none tracking-tight">Description</h3>
                <p className="leading-none tracking-tight">{credential.description}</p>
            </div>
            <div>
                <h3 className="mb-2 text-md font-bold leading-none tracking-tight">Updated At</h3>
                <p className="leading-none tracking-tight">{credential.updatedAt.toLocaleString()}</p>
            </div>
        </div>
    )
}