'use client'

import PageTitle from "@/components/pageTitle";
import { useCurrentOwner } from "@/hooks/useCurrentOwner";
import { useCurrentUser } from "@/hooks/useCurrentUser";
import { CredentialDetail } from "@/models";
import { useEffect, useState } from "react";
import CredentialDetailSummary from "./credentialDetailSummary";
import CredentialRequestSection from "./credentialRequestSection";
import SectionTitle from "@/components/sectionTitle";

type CredentialDetailWrapperProps = {
    id: string
}

export default function CredentialDetailWrapper({ id }: CredentialDetailWrapperProps) {
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
        <div className='container p-6 mx-auto mt-3'>
            <PageTitle title='Credential Detail' />
            {credential.id > 0 && <CredentialDetailSummary credential={credential} />}
            <div className="grid grid-cols-2 gap-2 mt-6">
                <div><SectionTitle title='Credential Issuances' /></div>
                {credential.id > 0 && <CredentialRequestSection credential={credential} />}
            </div>
        </div>
    );
}