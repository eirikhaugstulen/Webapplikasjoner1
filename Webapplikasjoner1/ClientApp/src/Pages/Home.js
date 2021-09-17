import React from 'react';
import {Button, Card, CardBody, Nav, NavItem, TabContent, TabPane, NavLink} from "reactstrap";
import explore from '../Media/explore.svg'

export const Home = () => {

    return (
        <div>
                <CardBody className={'d-flex flex-column justify-content-center text-center'}>
                    <h3 className={'m-3 font-italic'}>Bestill din drømmetur</h3>
                    <div className={'row mb-5'}>
                        <div className={'col d-flex flex-column p-5 mt-5'}>
                            <h5 style={{color: '#720505'}}>Hvor vil du reise?</h5>
                            <div className={'p-3'}>
                                <p className={'lh-lg text-muted'}>
                                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse id ligula placerat, convallis magna at, vulputate ligula. Suspendisse consequat mauris vel lorem fringilla, quis rhoncus lectus mollis. Duis et justo sit amet
                                </p>
                            </div>
                            <div className={'d-flex justify-content-center'}>
                                <Button outline color={'primary'}>Bestill</Button>
                            </div>
                        </div>
                        <div className={'col-6 p-5'}>
                            {/* Bilde hentet fra undraw.co */}
                            {/* https://undraw.co/illustrations */}
                            <img alt={'Explore'} src={explore} className={'img-fluid'} />
                        </div>
                    </div>
                    
                </CardBody>
        </div>
    );
}
