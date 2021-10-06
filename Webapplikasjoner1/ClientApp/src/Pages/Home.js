import React from 'react';
import {Button, CardBody, Col} from "reactstrap";
import explore from '../Media/explore.svg'
import history from "../history";

export const Home = () => {

    return (
        <div>
            <CardBody className={'d-flex flex-column justify-content-center text-center'}>
                <h3 className={'m-3 font-italic'}>Bestill din drømmetur</h3>
                <div className={'row mb-5'}>
                    <Col sm={12} md={6} className={'d-flex flex-column p-5 mt-2'}>
                        <h4 style={{color: '#720505'}}>Hvor vil du reise?</h4>
                        <div className={'p-3'}>
                            <p className={'lh-lg text-muted'}>
                                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse id ligula placerat, convallis magna at, vulputate ligula. Suspendisse consequat mauris vel lorem fringilla, quis rhoncus lectus mollis. Duis et justo sit amet
                            </p>
                        </div>
                        <div className={'d-flex flex-column justify-content-center'} style={{gap: '10px'}}>
                            <Button
                                outline
                                color={'primary'}
                                onClick={() => history.push('/bestill')}
                            >
                                Fortsett som gjest
                            </Button>
                            
                            <Button
                                color={'primary'}
                                disabled
                            >
                                Logg inn
                            </Button>
                            
                            <Button
                                outline
                                disabled
                            >
                                Registrer ny bruker
                            </Button>
                            
                        </div>
                    </Col>
                    <Col sm={12} md={6} className={'p-5'}>
                        {/* Bilde hentet fra undraw.co */}
                        {/* https://undraw.co/illustrations */}
                        <img alt={'Explore'} src={explore} className={'img-fluid'} />
                    </Col>
                </div>

            </CardBody>
        </div>
    );
}
