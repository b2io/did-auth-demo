

import AuthGuard from '@/components/authGuard';
import CredentialDetailSummary from '@/app/credential/[id]/credentialDetailSummary';
import React from 'react';
import PageTitle from '@/components/pageTitle';

type CredentialDetailPageProps = {
    params: {
        id: string
    }
}

export default function CredentialDetailPage({ params }: CredentialDetailPageProps) {
    return(
        <AuthGuard>
            <div className='container p-6 mx-auto my-3'>
                <PageTitle title='Credential Detail' />
                <CredentialDetailSummary id={params.id} />
            </div>
        </AuthGuard>
    )
}