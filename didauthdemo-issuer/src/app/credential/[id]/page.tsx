

import AuthGuard from '@/components/authGuard';
import CredentialDetailSummary from '@/app/credential/[id]/credentialDetailSummary';
import React from 'react';
import PageTitle from '@/components/pageTitle';
import SectionTitle from '@/components/sectionTitle';
import CredentialRequestSection from './credentialRequestSection';
import CredentialDetailWrapper from './credentialDetailWrapper';

type CredentialDetailPageProps = {
    params: {
        id: string
    }
}

export default function CredentialDetailPage({ params }: CredentialDetailPageProps) {

    return(
        <AuthGuard>
            <CredentialDetailWrapper id={params.id} />
        </AuthGuard>
    )
}