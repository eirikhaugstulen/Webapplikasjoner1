import React from "react";

export const ErrorScreen = ({ error }) => (
    <div className={'d-flex justify-content-center m-5'}>
        <div className={'text-center'}>
            <p className={'text-danger'}>Det har skjedd en feil</p>
            {JSON.stringify(error, null, 2)}
        </div>
    </div>
)