import React, {useState} from "react";
import {Button} from "reactstrap";
import history from "../../history";

const pushToPage = (page) => history.push(`/admin/${page}`);

export const NavigationButton = ({ tittel, tekst, page }) => (
        <div
            className={'d-flex justify-content-center rounded p-4'}
            style={{backgroundColor: '#E5E7EB'}}
        >
            <div className={'text-center'}>
                <h5>{tittel}</h5>
                <p>
                    {tekst}
                </p>
                {page && (<Button onClick={() => pushToPage(page)}>Administrer</Button>)}
            </div>
        </div>
    )