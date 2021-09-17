import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';
import bakgrunn from '../Media/bakgrunn.svg'

const styles = {
    width: '100%',
    zIndex: -3,
    aspectRatio: '960/250',
    position: 'fixed',
    bottom: 0,
}
    
export class Layout extends Component {
  static displayName = Layout.name;
  

  render () {
    return (
      <div>
        <NavMenu />
        <Container>
          {this.props.children}
        </Container>
        <img src={bakgrunn} alt={'Bakgrunn'} style={styles} />
      </div>
    );
  }
}
