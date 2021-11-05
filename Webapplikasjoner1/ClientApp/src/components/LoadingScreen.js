import React from "react";
import {Spinner} from "reactstrap";

export const LoadingScreen = () => (
    <div className={'d-flex justify-content-center m-5'}>
        <div className={'text-center'}>
            <Spinner
                color="info"
                type="grow"
            >
                Laster inn...
            </Spinner>
            <p>Laster inn</p>
        </div>
    </div>
)