'use client'

import { useEffect, useState } from "react";
import { useRouter } from 'next/navigation'
import Link from "next/link";
import { useCurrentUser } from "@/hooks/useCurrentUser";
import { useCurrentOwner } from "@/hooks/useCurrentOwner";
import PageTitle from "@/components/pageTitle";

function Credential({credential}: any) {
    return (
        <div className="card w-auto bg-base-100 shadow-xl">
            <div className="card-body">
                <h2 className="card-title">{credential.name}</h2>
                <p>{credential.description}</p>
                <div className="card-actions justify-end">
                    <Link href={`/credential/${credential.id}`}>
                        <button className="btn btn-primary">View</button>
                    </Link>
                </div>
            </div>
        </div>
    )
}

export default function CredentialListPage() {
    const router = useRouter()
    const [credentials, setCredentials] = useState([]);
    const { currentUser } = useCurrentUser();
    const { token } = useCurrentOwner(currentUser);

    useEffect(() => {
        console.log(token);
        if(token === null) return;

        fetch(`http://localhost:5196/api/get-credential-schemas/${token.unique_name}/list`, { cache: 'no-store'})
        .then(res => {
            if(res.ok) {
                res.json().then(resObj => {
                    let credentialList = resObj.map((credential: any) => {
                        return (
                            <Credential key={credential.id} credential={credential} />
                        )
                    });

                    setCredentials(credentialList);
                });
            }
        });
    }, [token]);

    function createCredential() {
        router.push('/credential/create');
    } 

    return (
        <div>
            <div className="grid grid-cols-2 gap-2 mb-3">
                <PageTitle title='Credentials' />
                <div>
                    <button className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 mr-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800 w-20 float-right" onClick={createCredential}>Create</button>
                </div>
            </div>
            {credentials && credentials.length > 0 
                ? <div className="grid grid-cols-4 gap-4">{credentials}</div> 
                : <p>No Credentials</p>}
        </div>
    )
}